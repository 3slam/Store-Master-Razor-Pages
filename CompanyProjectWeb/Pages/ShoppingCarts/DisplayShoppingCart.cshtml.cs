using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMaster.DataAccess.Repository;
using StoreMaster.DataAccess.Repository.IRepository;
using StoreMaster.Models;
using StoreMaster.Models.ViewModels;

namespace StoreMaster.Pages.ShoppingCart
{
    [BindProperties]
    public class DisplayShoppingCartModel (IUnitOfWork unitOfWork,UserManager<IdentityUser> userManager): PageModel
    {
        public ShoppingCartViewModel ShoppingCartViewModel { get; set; }
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly UserManager<IdentityUser> userManager = userManager;

       
        public void OnGet()
        {
			var allProductsInCart =  unitOfWork.ShoppingCart.GetAll(c => c.UserId == userManager.GetUserId(User)).ToList();
            ShoppingCartViewModel = new ShoppingCartViewModel();
            ShoppingCartViewModel.ShoppingCartProducts = allProductsInCart;
        }

        public IActionResult OnPostPlus(int cartId)
        {
            var cart =  unitOfWork.ShoppingCart.GetValue(u => u.Id == cartId);
            var x =  unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            return RedirectToPage();
         
        }

        public IActionResult OnPostMins(int cartId)
        {
            var cart = unitOfWork.ShoppingCart.GetValue(u => u.Id == cartId);
            var x = unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            return RedirectToPage();
        }

        public IActionResult OnPostRemove(int cartId)
        {

            Cart cartItem = unitOfWork.ShoppingCart.GetValue(c => c.Id == cartId);
             unitOfWork.ShoppingCart.Remove(cartItem);
            return RedirectToPage();
        }
    }
}
