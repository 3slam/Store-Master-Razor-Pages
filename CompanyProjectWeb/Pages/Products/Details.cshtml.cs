using StoreMaster.DataAccess.Data;
using StoreMaster.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMaster.DataAccess.Repository.IRepository;
using StoreMaster.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using StoreMaster.Models.ViewModels;
using StoreMaster.DataAccess.Repository;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
namespace StoreMaster.Pages.Products
{

    [BindProperties]
    [Authorize]
    public class DatailsModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment , SignInManager<IdentityUser> signInManager) : PageModel
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IWebHostEnvironment  webHostEnvironment = webHostEnvironment;
        private readonly SignInManager<IdentityUser> signInManager = signInManager;

        public Cart Cart    { get; set; }

        public void OnGet(int ProductId)
        {
            if (ProductId != null && ProductId != 0)
            {
                Product Product = unitOfWork.Product.GetValue(p => p.ProductId == ProductId);

                Cart cartFromDb = unitOfWork.ShoppingCart.GetValue(
                    u => u.UserId == signInManager.UserManager.GetUserId(User) && u.ProductId == Product.ProductId);
 
                if(cartFromDb!= null)
                {
                    Cart = cartFromDb;
                }
                else
                {
                    Cart = new Cart()
                    {
                        ProductId = Product.ProductId,
                        Product = Product,
                        Count = 0,
                        UserId = signInManager.UserManager.GetUserId(User)
                    };
                }
            }
        }

         public IActionResult OnPost()
         {
                 Cart cartFromDb = unitOfWork.ShoppingCart.GetValue(
                    u => u.UserId == signInManager.UserManager.GetUserId(User) && u.ProductId == Cart.ProductId);

                if (cartFromDb != null)
                {
                    //shopping cart exists
                    cartFromDb.Count = Cart.Count;
                    unitOfWork.ShoppingCart.Update(cartFromDb);
                    return RedirectToPage("/Index");
                }
                else
                {
                    unitOfWork.ShoppingCart.Add(Cart);
                    TempData["success"] = "Cart Updated deleted successfully";
                    return RedirectToPage("/Index");
                }

          
            return Page();
        }
    }
}
