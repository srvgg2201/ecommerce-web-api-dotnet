using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public string PaymentMode { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public double? GrandTotal { get; set; }

        public DateTime OrderDate { get; set; }
        public bool PaymentSuccessful { get; set; }
    }
}
