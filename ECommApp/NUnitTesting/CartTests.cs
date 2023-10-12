using DAL;
using DAL.Models;
using BLL.Repositories;
using BLL.Repositories.Interfaces;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace NUnitTesting
{
    public class CartTests
    {
        DataContext context;
        ICartRepo cartRepo;
        IUserRepo userRepo;
        IitemRepo itemRepo;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: "ECommTest")
           .Options;
            context = new DataContext(options);
            cartRepo = new CartRepo(context);
            userRepo = new UserRepo(context);
            itemRepo = new ItemRepo(context);
            context.Categories.Add(new Category { Id = 1, Name = "C1" });
            userRepo.AddUser(new User { Id = 1, Name = "U1", Address = "A1", Email = "E1" });
            context.Items.Add(new Item { Id = 1, Name = "I1", Category = context.Categories.Find(1), CategoryId = 1, CreatedDate = System.DateTime.UtcNow, Description = "D1", IsAvailable = true, Price = 500, Quantity = 10 });
            context.Items.Add(new Item { Id = 2, Name = "I2", Category = context.Categories.Find(1), CategoryId = 1, CreatedDate = System.DateTime.UtcNow, Description = "D2", IsAvailable = true, Price = 600, Quantity = 5 });
            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Dispose();
        }

        [Test]
        [Order(1)]
        public void GetCartTest()
        {
            var cart = cartRepo.ViewCart(1);
            Assert.AreEqual(0, cart.GrandTotal);
            Assert.AreEqual(0, cart.TotalItemCount);
        }


        [Test]
        [Order(2)]
        public void AddCartItemTest()
        {

            cartRepo.AddItemById(1, 1, 10);
            cartRepo.AddItemById(1, 2, 5);
            var cart = cartRepo.ViewCart(1);
            Assert.AreEqual(8000, cart.GrandTotal);
            Assert.AreEqual(15, cart.TotalItemCount);
        }
        [Test]
        [Order(3)]
        public void RemoveCartItemTest()
        {
            cartRepo.RemoveCartItem(1, 1);
            var cart = context.Carts.Find(1);
            Assert.AreEqual(5, cart.CartItemCount);
            Assert.AreEqual(3000, cart.GrandTotal);
        }

        [Test]
        [Order(4)]
        public void EmptyCartTest()
        {
            cartRepo.EmptyCart(1);
            var cart = cartRepo.ViewCart(1);
            Assert.AreEqual(0, cart.TotalItemCount);
            Assert.AreEqual(0, cart.GrandTotal);
        }

        [Test]
        [Order(5)]
        public void CheckoutTest()
        {
            cartRepo.AddItemById(1, 1, 10);
            cartRepo.CheckOut(1, 5000, "COD");
            var order = context.Orders.Find(1);
            var item = itemRepo.GetItemById(1);
            Assert.AreEqual(1, order.UserId);
            Assert.AreEqual(5000, order.GrandTotal);
            Assert.AreEqual(0, item.Quantity);
            Assert.AreEqual(false, item.IsAvailable);
        }

    }
}