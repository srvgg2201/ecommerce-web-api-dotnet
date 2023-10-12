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
    public class OrderRepo : IOrderRepo
    {
        private readonly DataContext _db;
        public OrderRepo(DataContext db) 
        {
            _db = db;
        }

        public List<OrderView> GetAllOrders()
        {
            List<OrderView> orders = new List<OrderView>();
            var dbOrders = _db.Orders.ToList();
            foreach(var order in dbOrders)
            {
                var user = _db.Users.Where(x => x.Id == order.UserId).FirstOrDefault();
                var orderItemViewList = new List<OrderItemView>();
                var orderItems = _db.OrderItems.Where(x => x.OrderId == order.Id).ToList();
                foreach(var item in orderItems)
                {
                    var res = (from orderItem in _db.OrderItems
                               join itm in _db.Items on orderItem.ItemId equals itm.Id
                               select new OrderItemView
                               {
                                   ItemId = itm.Id,
                                   ItemCount = orderItem.ItemCount,
                                   ItemName = itm.Name,
                                   ItemPrice = itm.Price,
                                   ItemTotal = orderItem.ItemTotal
                               }).FirstOrDefault();
                    orderItemViewList.Add(res);
                }
                OrderView orderView = new OrderView
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    UserId = order.UserId,
                    UserName = user.Name,
                    UserAddress = user.Address,
                    OrderItems = orderItemViewList,
                    ModeOfPayment = order.PaymentMode,
                    GrandTotal = order.GrandTotal
                };
                orders.Add(orderView);    
            }
            return orders;
            
           
        }

        public OrderView GetByOrderId(int orderid)
        {
            var order = _db.Orders.Where(x => x.Id == orderid).FirstOrDefault();
            var user = _db.Users.Where(x => x.Id == order.UserId).FirstOrDefault();
            var orderItemViewList = new List<OrderItemView>();
            var orderItems = _db.OrderItems.Where(x => x.OrderId == orderid).ToList();
            foreach (var item in orderItems)
            {
                var res = (from orderItem in _db.OrderItems
                           join itm in _db.Items on orderItem.ItemId equals itm.Id
                           select new OrderItemView
                           {
                               ItemId = itm.Id,
                               ItemCount = orderItem.ItemCount,
                               ItemName = itm.Name,
                               ItemPrice = itm.Price,
                               ItemTotal = orderItem.ItemTotal
                           }).FirstOrDefault();
                orderItemViewList.Add(res);
            }
            OrderView orderView = new OrderView
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                UserId = order.UserId,
                UserName = user.Name,
                UserAddress = user.Address,
                OrderItems = orderItemViewList,
                ModeOfPayment = order.PaymentMode,
                GrandTotal = order.GrandTotal
            };
            return orderView;
        }

        public List<OrderView> GetOrderByUserId(int userid)
        {
            List<OrderView> orders = new List<OrderView>();
            var dbOrders = _db.Orders.Where(x => x.UserId == userid).ToList();
            foreach (var order in dbOrders)
            {
                var user = _db.Users.Where(x => x.Id == order.UserId).FirstOrDefault();
                var orderItemViewList = new List<OrderItemView>();
                var orderItems = _db.OrderItems.Where(x => x.OrderId == order.Id).ToList();
                foreach (var item in orderItems)
                {
                    var res = (from orderItem in _db.OrderItems
                               join itm in _db.Items on orderItem.ItemId equals itm.Id
                               select new OrderItemView
                               {
                                   ItemId = itm.Id,
                                   ItemCount = orderItem.ItemCount,
                                   ItemName = itm.Name,
                                   ItemPrice = itm.Price,
                                   ItemTotal = orderItem.ItemTotal
                               }).FirstOrDefault();
                    orderItemViewList.Add(res);
                }
                OrderView orderView = new OrderView
                {
                    Id = order.Id,
                    OrderDate = order.OrderDate,
                    UserId = order.UserId,
                    UserName = user.Name,
                    UserAddress = user.Address,
                    OrderItems = orderItemViewList,
                    ModeOfPayment = order.PaymentMode,
                    GrandTotal = order.GrandTotal
                };
                orders.Add(orderView);
            }
            return orders;
        }

        
    }
}
