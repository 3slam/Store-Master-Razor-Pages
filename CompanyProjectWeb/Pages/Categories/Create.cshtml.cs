using StoreMaster.DataAccess.Data;
using StoreMaster.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMaster.DataAccess.Repository.IRepository;

namespace StoreMaster.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {

        public Category Category { get; set; }
        public IUnitOfWork unitOfWork;
        public CreateModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(IFormFile file)
        { 
                try{
                    unitOfWork.Category.Add(Category);
                    TempData["success"] = "Category created successfully";
                    return RedirectToPage("DisplayAllCategories");
                }
                catch(Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToPage("Create");
                }
        }


         
    }
}
