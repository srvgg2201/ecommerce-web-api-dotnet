using DAL;
using DAL.Models;
using BLL.Repositories;
using BLL.Repositories.Interfaces;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace NUnitTesting
{
    public class ItemTests
    {
        DataContext context;
        IitemRepo itemRepo;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: "ECommTest")
           .Options;
            context = new DataContext(options);
            itemRepo = new ItemRepo(context);
            context.Categories.Add(new Category { Id = 1, Name = "C1" });
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
        public void GetItemTest()
        {
            var items = itemRepo.GetAllItems();
            Assert.AreEqual(2,items.Count);
        }


        [Test]
        [Order(2)]
        public void UpdateItemTest()
        {

            var item = context.Items.Find(1);
            item.Name = "I3";
            item.Quantity = 0;
            itemRepo.UpdateItem(item);
            item = context.Items.Find(1);
            Assert.AreEqual("I3", item.Name);
            Assert.AreEqual(0, item.Quantity);
            Assert.AreEqual(false, item.IsAvailable);
        }
        [Test]
        [Order(3)]
        public void AddItemTest()
        {
            var item = new Item { Id = 3, Name = "I3", Category = context.Categories.Find(1), CategoryId = 1, CreatedDate = System.DateTime.UtcNow, Description = "I3", IsAvailable = true, Price = 600, Quantity = 5 };
            itemRepo.AddItem(item);
            var items = itemRepo.GetAllItems();
            var actualItem = context.Items.Find(3);
            Assert.AreEqual(3, items.Count);
            Assert.AreEqual(item, actualItem);
        }

        [Test]
        [Order(4)]
        public void DeleteItemTest()
        {
            itemRepo.DeleteItem(3);
            var items = itemRepo.GetAllItems();
            Assert.AreEqual(2, items.Count);
        }

    }
}