using DAL;
using DAL.Models;
using BLL.Repositories;
using BLL.Repositories.Interfaces;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace NUnitTesting
{
    public class UserTests
    {
        DataContext context;
        IUserRepo userRepo;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: "ECommTest")
           .Options;
            context = new DataContext(options);
            userRepo = new UserRepo(context);
            context.Users.Add(new User { Id = 1, Name = "U1", Address = "A1", Email = "E1" });
            context.Users.Add(new User { Id = 2, Name = "U2", Address = "A2", Email = "E2" });
            
            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Dispose();
        }

        [Test]
        [Order(1)]
        public void GetUserTest()
        {
            var users = userRepo.GetAllUsers();
            Assert.AreEqual(2, users.Count);
        }


        [Test]
        [Order(2)]
        public void UpdateUserTest()
        {

            var user = context.Users.Find(1);
            user.Name = "U3";
            user.Address = "A3";
            userRepo.UpdateUser(user);
            user = context.Users.Find(1);
            Assert.AreEqual("U3", user.Name);
            Assert.AreEqual("A3", user.Address);
        }
        [Test]
        [Order(3)]
        public void AddUserTest()
        {
            var user = new User();
            user.Id = 3;
            user.Name = "U3";
            user.Address = "A3";
            user.Email = "E3";
            userRepo.AddUser(user);
            var users = userRepo.GetAllUsers();
            user = context.Users.Find(3);
            Assert.AreEqual(users.Count, 3);
            Assert.AreEqual("U3", user.Name);
            Assert.AreEqual("A3", user.Address);
            Assert.AreEqual("E3", user.Email);
        }

        [Test]
        [Order(4)]
        public void DeleteUserTest()
        {
            userRepo.DeleteUser(3);
            var users = userRepo.GetAllUsers();
            Assert.AreEqual(2, users.Count);
        }

    }
}