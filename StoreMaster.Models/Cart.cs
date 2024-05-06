using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreMaster.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string UserId { get; set; }



        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }


        [Range(1, 100, ErrorMessage = "Please enter a value between 1 and 100")]
        [Required]
        public int Count { get; set; }

        [ForeignKey("UserId")]

        public virtual IdentityUser User { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
