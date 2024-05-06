
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMaster.DataAccess.Data;
using StoreMaster.DataAccess.Repository;
using StoreMaster.DataAccess.Repository.IRepository;
using StoreMaster.Models;

namespace StoreMaster.Pages.Categories
{
    public class DisplayAllCategoriesModel(IUnitOfWork unitOfWork) : PageModel
    {
 
        public List<Category> Categories { get; set; }
        public IUnitOfWork unitOfWork = unitOfWork;

        public void OnGet()
        {
            Categories = (List<Category>)unitOfWork.Category.GetAll().ToList();
        }

        
    }
}
