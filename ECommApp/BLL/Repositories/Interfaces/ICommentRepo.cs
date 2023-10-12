using DAL;
using DAL.Models;
using BLL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.Interfaces
{
    public interface ICommentRepo
    {
        public List<CommentView> GetCommentsByItemId(int itemid);
        public CommentView GetCommentsByCommentId(int commentid);

        public string Comment(CommentBody body);
        public string UpdateComment(int commentid, string comment);
        public string DeleteComment(int commentid);
    }
}
