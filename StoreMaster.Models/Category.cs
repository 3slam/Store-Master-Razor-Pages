using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
 
namespace StoreMaster.Models
{
    public class Category
    {
       
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter the category name.")]
        [MinLength(4,ErrorMessage = "The Name Must be more than 4 characters")]
        public string Name { get; set; }

        public string Description { get; set; }
 
        public virtual List<Product> products { get; set; }

    }
}
