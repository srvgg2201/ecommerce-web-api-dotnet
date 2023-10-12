using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Views
{
    public class CommentView
    { 
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int ParentId { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }
        public List<CommentView> Replies { get; set; } = new List<CommentView>();
    }
}
