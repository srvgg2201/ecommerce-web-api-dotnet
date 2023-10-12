using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Views
{
    public class OrderView
    {
        public int Id { get; set; }
        public int? UserId { get; set; }

        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public List<OrderItemView> OrderItems { get; set; }

        public double? GrandTotal { get; set; }

        public DateTime OrderDate { get; set; }

        public string ModeOfPayment { get; set; }



    }
}
