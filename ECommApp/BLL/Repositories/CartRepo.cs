using BLL.Views;
using BLL.Repositories.Interfaces;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class CartRepo : ICartRepo
    {
        private readonly DataContext _dbContext;
        public CartRepo(DataContext dataContext)
        {
            _dbContext = dataContext;
        }
        public string AddItemById(int userid, int itemid, int quantity)
        {
            var cart = _dbContext.Carts.Where(x => x.UserId == userid).FirstOrDefault();
            var item = _dbContext.Items.Where(x => x.Id == itemid).FirstOrDefault();
            if (item.IsAvailable == false) return "Item Out Of Stock";
            if (quantity <= item.Quantity)
            {
                var existingCartItem = _dbContext.CartItems.Where(x => x.Id == itemid).FirstOrDefault();
                if (existingCartItem != null && item != null)
                {
                    if (existingCartItem.ItemCount + quantity > item.Quantity) 
                        return "Item Quantity exceeding available quantity. Available units : " + item.Quantity.ToString();
                    existingCartItem.ItemCount += quantity;
                    existingCartItem.ItemTotal += item.Price * quantity;
                    _dbContext.CartItems.Update(existingCartItem);
                }

                else
                {
                    CartItem cartItem = new CartItem
                    {
                        CartId = cart.Id,
                        Item = item,
                        ItemId = item.Id,
                        ItemCount = quantity,
                        ItemTotal = item.Price * quantity
                    };
                    cart.CartItems.Add(cartItem);
                    _dbContext.CartItems.Add(cartItem);
                }
                
                cart.CartItemCount += quantity;
                cart.GrandTotal += quantity * item.Price;
                _dbContext.Carts.Update(cart);
                _dbContext.SaveChanges();
                return "Item Added To Cart";
            }

            return "Enter Quantity less than or equal to " + item.Quantity.ToString();
            
        }

       

        public string EmptyCart(int userid)
        {
            var cart = _dbContext.Carts.Where(x => x.UserId == userid).FirstOrDefault();
            var cartItems = _dbContext.CartItems.Where(x => x.CartId == cart.Id).ToList();
            foreach (var cartItem in cartItems)
            {
                _dbContext.CartItems.Remove(cartItem);
            }
            cart.CartItems.Clear();
            cart.CartItemCount = 0;
            cart.GrandTotal = 0;
            _dbContext.Carts.Update(cart);
            _dbContext.SaveChanges();
            return "Cart is Empty";

        }

        public CartView RemoveCartItem(int userid, int itemid)
        {
            var cart = _dbContext.Carts.Where(x => x.UserId == userid).FirstOrDefault();
            var cartItem = _dbContext.CartItems.Where(x => x.ItemId == itemid && cart.UserId == userid ).FirstOrDefault();
            _dbContext.CartItems.Remove(cartItem);
            cart.CartItemCount -= cartItem.ItemCount;
            cart.GrandTotal -= cartItem.ItemTotal;
            _dbContext.SaveChanges();
            return ViewCart(userid);
        }

        public string UpdateCart(int userid, int itemid, int quantity)
        {
            var cart = _dbContext.Carts.Where(x => x.UserId == userid).FirstOrDefault();
            var cartItem = _dbContext.CartItems.Where(x => x.ItemId == itemid && x.CartId == cart.Id).FirstOrDefault();
            var item = _dbContext.Items.Where(x => x.Id == itemid).FirstOrDefault();
            if(cartItem == null) { return "Item with item ID " + itemid.ToString() + " Does Not Exist in Cart"; }
            if(quantity <= item.Quantity)
            {
                cart.CartItems.Remove(cartItem);
                cart.CartItemCount -= cartItem.ItemCount;
                cart.GrandTotal -= cartItem.ItemTotal;
                cartItem.ItemCount = quantity;
                cartItem.ItemTotal = quantity * item.Price;
                cart.CartItemCount += cartItem.ItemCount;
                cart.GrandTotal += cartItem.ItemTotal;
                _dbContext.CartItems.Update(cartItem);
                cart.CartItems.Add(cartItem);
                _dbContext.Carts.Update(cart);
                _dbContext.SaveChanges();
                return "Cart Updated Successfully";
            }
            return "Invalid Quantity";
            


        }

        public CartView ViewCart(int userid)
        {
            var cart = _dbContext.Carts.Where(x => x.UserId == userid).FirstOrDefault();
            var user = _dbContext.Users.Where(x => x.Id == userid).FirstOrDefault();
            var cartItems = _dbContext.CartItems.Where(x => x.CartId == cart.Id).ToList();
            List<CartItemView> cartItemsView = new List<CartItemView>();
            foreach(var cartItem in cartItems)
            {
                var res = (from item in _dbContext.Items
                                             join crtItem in _dbContext.CartItems on item.Id equals cartItem.ItemId
                                             select new CartItemView
                                             {
                                                 ItemId = cartItem.ItemId,
                                                 ItemName = item.Name,
                                                 UserId = userid,
                                                 ItemCount = cartItem.ItemCount,
                                                 ItemPrice = item.Price,
                                                 ItemTotal = cartItem.ItemTotal
                                             }).FirstOrDefault();
                
                cartItemsView.Add(res);
            }
 
            CartView cartView = new CartView()
                                {
                                    CartId = cart.Id,
                                    UserId = userid,
                                    UserName = user.Name,
                                    cartItems = cartItemsView,
                                    TotalItemCount = cart.CartItemCount,
                                    GrandTotal = cart.GrandTotal
                                };
            return cartView;
        }

        public string CheckOut(int userid, double amount, string mode)
        {
            var cart = _dbContext.Carts.Where(x => x.UserId == userid).FirstOrDefault();
            if (cart == null) return "Invalid User ID";
            if (cart.CartItemCount == 0) return "Cart is empty. Add Items to Cart First";
            if(cart.GrandTotal != amount)
            {
                return "Invalid Amount. Payment Failed.";
            }
            else
            {
                var cartItems = _dbContext.CartItems.Where(x => x.CartId == cart.Id).ToList();
                var user = _dbContext.Users.Where(x => x.Id == cart.UserId).FirstOrDefault();
                Order order = new Order();
                order.User = user;
                order.PaymentMode = mode;
                order.PaymentSuccessful = true;
                order.UserId = user.Id;
                order.OrderDate = DateTime.UtcNow;
                order.GrandTotal = cart.GrandTotal;
                List<OrderItem> orderItems = new List<OrderItem>();
                foreach (var item in cartItems)
                {
                    var storeItem = _dbContext.Items.Find(item.ItemId);
                    storeItem.Quantity -= item.ItemCount;
                    if (storeItem.Quantity == 0) storeItem.IsAvailable = false;
                    _dbContext.Items.Update(storeItem);
                    OrderItem orderItem = new OrderItem()
                    {
                        ItemId = item.Id,
                        Item = item.Item,
                        ItemCount = item.ItemCount,
                        ItemTotal = item.ItemTotal,
                        Cart = cart,
                        CartId = cart.Id,
                        Order = order,
                        OrderId = order.Id
                    };
                    orderItems.Add(orderItem);
                    _dbContext.OrderItems.Add(orderItem);
                    _dbContext.CartItems.Remove(item);
                }
                order.OrderItems = orderItems;
                _dbContext.Orders.Add(order);
                cart.CartItems.Clear();
                cart.CartItemCount = 0;
                cart.GrandTotal = 0;
                _dbContext.Carts.Update(cart);
                _dbContext.SaveChanges();

                return "Order Has Been Placed Successfully";

            }
        }


    }
}
