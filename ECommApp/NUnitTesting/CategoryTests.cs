using DAL;
using DAL.Models;
using BLL.Repositories;
using BLL.Repositories.Interfaces;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace NUnitTesting
{
    public class CategoryTests
    {
        DataContext context;
        ICategoryRepo categoryRepo;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: "ECommTest")
           .Options;
            context = new DataContext(options);
            // Insert seed data into the database using one instance of the context
            categoryRepo = new CategoryRepo(context);
            context.Categories.Add(new Category { Id = 1, Name = "C1" });
            context.Categories.Add(new Category { Id = 2, Name = "C2" });
            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Dispose();
        }

        [Test]
        [Order(1)]
        public void GetCategoryTest()
        {
            var category = categoryRepo.GetCategories();
            Assert.AreEqual(2, category.Count);
        }


        [Test]
        [Order(2)]
        public void UpdateCategoryTest()
        {

            var category = context.Categories.Find(1);
            category.Name = "C3";
            categoryRepo.UpdateCategory(category);
            category = context.Categories.Find(1);
            Assert.AreEqual("C3", category.Name);
        }
        [Test]
        [Order(3)]
        public void AddCategoryTest()
        {
            var category = new Category();
            category.Id = 3;
            category.Name = "c3";
            categoryRepo.AddCategory(category);
            var categories = categoryRepo.GetCategories();
            Assert.AreEqual(categories.Count, 3);
            Assert.AreEqual("c3", context.Categories.Find(3).Name);
        }

        [Test]
        [Order(4)]
        public void DeleteCategoryTest()
        {
            categoryRepo.RemoveCategory(3);
            var category = categoryRepo.GetCategories();
            Assert.AreEqual(2, category.Count);
        }

    }
}