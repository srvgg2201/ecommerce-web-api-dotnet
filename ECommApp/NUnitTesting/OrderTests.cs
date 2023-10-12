using DAL;
using DAL.Models;
using BLL.Repositories;
using BLL.Repositories.Interfaces;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace NUnitTesting
{
    public class OrderTests
    {
        DataContext context;
        ICartRepo cartRepo;
        IUserRepo userRepo;
        IitemRepo itemRepo;
        IOrderRepo orderRepo;
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
            orderRepo = new OrderRepo(context);
            context.Categories.Add(new Category { Id = 1, Name = "C1" });
            userRepo.AddUser(new User { Id = 1, Name = "U1", Address = "A1", Email = "E1" });
            userRepo.AddUser(new User { Id = 2, Name = "U2", Address = "A2", Email = "E2" });
            context.Items.Add(new Item { Id = 1, Name = "I1", Category = context.Categories.Find(1), CategoryId = 1, CreatedDate = System.DateTime.UtcNow, Description = "D1", IsAvailable = true, Price = 500, Quantity = 10 });
            context.Items.Add(new Item { Id = 2, Name = "I2", Category = context.Categories.Find(1), CategoryId = 1, CreatedDate = System.DateTime.UtcNow, Description = "D2", IsAvailable = true, Price = 600, Quantity = 5 });
            context.SaveChanges();
            cartRepo.AddItemById(1, 1, 5);
            cartRepo.CheckOut(1, 2500, "COD");
            cartRepo.AddItemById(2, 2, 5);
            cartRepo.CheckOut(2, 3000, "UPI");
            
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Dispose();
        }

        [Test]
        [Order(1)]
        public void GetOrdersTest()
        {
            var orders = orderRepo.GetAllOrders();
            Assert.AreEqual(2, orders.Count);
        }

        [Test]
        [Order(2)]
        public void GetOrdersByUserIdTest()
        {
            var orders = orderRepo.GetOrderByUserId(1);
            Assert.AreEqual(1, orders.Count);
            Assert.AreEqual(1, orders[0].UserId);
        }

        [Test]
        [Order(3)]
        public void GetOrderByIdTest()
        {
            var order = context.Orders.Find(1);
            Assert.AreEqual(2500, order.GrandTotal);
            Assert.AreEqual(true, order.PaymentSuccessful);
            Assert.AreEqual(1, order.UserId);
        }
    }
}
