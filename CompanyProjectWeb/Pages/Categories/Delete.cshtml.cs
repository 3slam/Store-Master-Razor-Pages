 
using StoreMaster.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMaster.DataAccess.Data;
using StoreMaster.DataAccess.Repository.IRepository;

namespace StoreMaster.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
 

        public Category Category { get; set; }
        public IUnitOfWork unitOfWork;
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void OnGet(int  CategoryId)
        {
            if (CategoryId != null && CategoryId != 0)
            {
                Category = unitOfWork.Category.GetValue(c => c.CategoryId == CategoryId);
            }
        }

        public IActionResult OnPost()
        {
            var  obj = unitOfWork.Category.GetValue(c => c.CategoryId == Category.CategoryId);
            if (obj == null)
            {
                return NotFound();
            }
            unitOfWork.Category.Remove(obj);
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("DisplayAllCategories");
        }
    }

}
