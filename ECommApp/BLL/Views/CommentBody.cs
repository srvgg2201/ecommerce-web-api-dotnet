using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Views
{
    public class CommentBody
    {
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public int ParentId { get; set; }
        public string Comment { get; set; }
    }
}
