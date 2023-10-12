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
    public class CommentRepo : ICommentRepo
    {
        private readonly DataContext _db;
        public CommentRepo(DataContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Utility Recursive Function returning List of Comments for an Item
        /// </summary>
        /// <param name="itemid"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        private List<CommentView> GetCommentsUtil(int itemid, int parentid)
        {
            List<CommentView> commentsView = new List<CommentView>();
            var comments = _db.Comments.Where(x => x.ItemId == itemid && x.ParentId == parentid).ToList();
            var item = _db.Items.Where(x => x.Id == itemid).FirstOrDefault();

            // Base Case
            if(comments.Count == 0) return new List<CommentView>(){ };

            foreach(var comment in comments)
            {
                CommentView commentView = new CommentView();
                var user = _db.Users.Where(x => comment.UserId == x.Id).FirstOrDefault();
                commentView.Id = comment.Id;
                commentView.ParentId = parentid;
                commentView.UserId = user.Id;
                commentView.UserName = user.Name;
                commentView.ItemId = itemid;
                commentView.ItemName = item.Name;
                commentView.Content = comment.Content;
                commentView.Date = comment.Date;
                commentView.Replies = GetCommentsUtil(itemid, comment.Id);
                commentsView.Add(commentView);
            }
            
            return commentsView;
        }
        public string Comment(CommentBody body)
        {
            var user = _db.Users.Where(x => x.Id == body.UserId).FirstOrDefault();
            if (user == null) return "Invalid User ID";
            var item = _db.Items.Find(body.ItemId);
            if (item == null) return "Invalid Item ID";
            
            if(body.ParentId != 0)
            {
                var parent = _db.Comments.Where(x => x.Id == body.ParentId).FirstOrDefault();
                if (parent == null) return "Invalid Parent ID";
            }
            Comment comment = new Comment
            {
                UserId = user.Id,
                User = user,
                ItemId = item.Id,
                Item = item,
                Date = DateTime.UtcNow,
                Content = body.Comment,
                ParentId = body.ParentId,
            };
            _db.Comments.Add(comment);
            _db.SaveChanges();
            return "Comment Added Successfully";
        }

        public string UpdateComment(int commentid, string comment)
        {
            var oldComment = _db.Comments.Where(x => x.Id == commentid).FirstOrDefault();
            if (oldComment == null) return "Comment Does Not Exist";
            oldComment.Content = comment;
            _db.Comments.Update(oldComment);
            _db.SaveChanges();
            return "Comment Updated Successfully";
        }

        public string DeleteComment(int commentid)
        {
            var comment = _db.Comments.Where(x => x.Id == commentid).FirstOrDefault();
            if (comment == null) return "No Comment Found/ Invalid Comment ID";
            _db.Comments.Remove(comment);
            _db.SaveChanges();
            return "Comment Deleted Successfully";
        }

        public List<CommentView> GetCommentsByItemId(int itemid)
        {
            return GetCommentsUtil(itemid, 0);
        }

        public CommentView GetCommentsByCommentId(int commentid)
        { 
            var comment = _db.Comments.Find(commentid);
            if (comment == null) return null;
            var user = _db.Users.Where(x => x.Id == comment.UserId).FirstOrDefault();
            var item = _db.Items.Where(x => x.Id == comment.ItemId).FirstOrDefault();
            CommentView commentView = new CommentView();
            commentView.Id = comment.Id;
            commentView.ParentId = comment.ParentId;
            commentView.UserId = user.Id;
            commentView.UserName = user.Name;
            commentView.ItemId = comment.ItemId;
            commentView.ItemName = item.Name;
            commentView.Content = comment.Content;
            commentView.Date = comment.Date;
            commentView.Replies = GetCommentsUtil(item.Id, comment.Id);
            
            return commentView;
        }

        
    }
}
