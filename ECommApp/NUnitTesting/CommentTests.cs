using DAL;
using DAL.Models;
using BLL.Repositories;
using BLL.Repositories.Interfaces;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;

namespace NUnitTesting
{
    public class CommentTests
    {
        DataContext context;
        IUserRepo userRepo;
        IitemRepo itemRepo;
        ICommentRepo commentRepo;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
           .UseInMemoryDatabase(databaseName: "ECommTest")
           .Options;
            context = new DataContext(options);
            commentRepo = new CommentRepo(context);
            userRepo = new UserRepo(context);
            itemRepo = new ItemRepo(context);
            context.Categories.Add(new Category { Id = 1, Name = "C1" });
            userRepo.AddUser(new User { Id = 1, Name = "U1", Address = "A1", Email = "E1" });
            userRepo.AddUser(new User { Id = 2, Name = "U2", Address = "A2", Email = "E2" });
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
        public void NewCommentTest()
        {
            commentRepo.Comment(new BLL.Views.CommentBody { ItemId = 1, ParentId = 0, UserId = 1, Comment = "Comment1" });
            commentRepo.Comment(new BLL.Views.CommentBody { ItemId = 1, ParentId = 1, UserId = 2, Comment = "Reply1" });
            var comments = commentRepo.GetCommentsByItemId(1);
            Assert.AreEqual(1, comments.Count);
            Assert.AreEqual(1, comments[0].Replies.Count);
        }

        [Test]
        [Order(2)]
        public void GetCommentsTest()
        {
            var comments = commentRepo.GetCommentsByItemId(1);
            Assert.AreEqual(1, comments.Count);
            Assert.AreEqual(1, comments[0].Replies.Count);
        }

        [Test]
        [Order(3)]
        public void UpdateCommentTest()
        {
            commentRepo.UpdateComment(1, "Edited");
            var comment = context.Comments.Find(1);
            Assert.AreEqual("Edited", comment.Content);
        }

        [Test]
        [Order(4)]
        public void DeleteComment()
        {
            commentRepo.DeleteComment(1);
            var comments = commentRepo.GetCommentsByItemId(1);
            Assert.AreEqual(0, comments.Count);
        }
    }
}
