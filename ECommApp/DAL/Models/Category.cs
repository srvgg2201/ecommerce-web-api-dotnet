using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50, ErrorMessage = "Max length is 50")]
        [Required]
        public string Name { get; set; }

    }
}
