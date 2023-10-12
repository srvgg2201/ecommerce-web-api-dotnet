using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType("int")]
        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price cannot be less than zero")]
        public double Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a number greater than 0")]
        public int Quantity { get; set; }

        public DateTime? CreatedDate { get; set; } = DateTime.Now.ToUniversalTime();

        public bool? IsAvailable { get; set; } 


    }
}
