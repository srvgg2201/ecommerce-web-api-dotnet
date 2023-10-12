using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Views
{
    public class CartView
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TotalItemCount { get; set; }
        public double? GrandTotal { get; set; }
        public List<CartItemView> cartItems { get; set; }

    }
}
