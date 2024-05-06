using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMaster.DataAccess.Repository.IRepository;
using StoreMaster.Models;

namespace StoreMaster.Pages
{
    public class IndexModel(IUnitOfWork unitOfWork) : PageModel
    {
        public List<Product> Products { get; set; }
        public IUnitOfWork unitOfWork = unitOfWork;

        public void OnGet()
        {
            Products = (List<Product>)unitOfWork.Product.GetAll().ToList();
        }

      
    }
}
