 
using StoreMaster.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMaster.DataAccess.Data;
using StoreMaster.DataAccess.Repository.IRepository;

namespace StoreMaster.Pages.Products
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
 

        public Product Product { get; set; }
        public IUnitOfWork unitOfWork;
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void OnGet(int  ProductId)
        {
            if (ProductId != null && ProductId != 0)
            {
                Product = unitOfWork.Product.GetValue(p => p.ProductId == ProductId);
            }
        }

        public IActionResult OnPost()
        {
            var  obj = unitOfWork.Product.GetValue(p => p.ProductId == Product.ProductId);
            if (obj == null)
            {
                return NotFound();
            }
            unitOfWork.Product.Remove(obj);
            TempData["success"] = "Product deleted successfully";
            return RedirectToPage("DisplayAllProducts");
        }
    }

}
