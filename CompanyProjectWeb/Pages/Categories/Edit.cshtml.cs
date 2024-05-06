using StoreMaster.DataAccess.Data;
using StoreMaster.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMaster.DataAccess.Repository.IRepository;

namespace StoreMaster.Pages.Categories
{

    [BindProperties]
    public class EditModel : PageModel
    {
        

        public Category Category { get; set; }
        public IUnitOfWork unitOfWork;
        public EditModel(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void OnGet(int CategoryId)
        {
            if (CategoryId != null && CategoryId != 0)
            {
                Category =  unitOfWork.Category.GetValue( c=> c.CategoryId == CategoryId);
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Category.Update(Category);
                TempData["success"] = "Category Edited successfully";
                return RedirectToPage("DisplayAllCategories");
            }

            return Page();
        }
    }

}
