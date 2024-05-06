using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMaster.Models
{
    public class Product
    {

        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }


        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        [Required]
        [Display(Name = "Product Image")]
        public string ImageUrl { get; set; }


        public int CategoryId { get; set; }


        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
