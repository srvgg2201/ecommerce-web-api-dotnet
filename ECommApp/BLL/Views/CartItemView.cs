using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Views
{
    public class CartItemView
    {
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public string ItemName { get; set; }
        public int ItemCount { get; set; }
        public double ItemPrice { get; set; }
        public double? ItemTotal { get; set; }
    }
}
