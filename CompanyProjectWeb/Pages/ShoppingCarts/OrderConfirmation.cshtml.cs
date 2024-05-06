using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMaster.DataAccess.Repository;
using StoreMaster.DataAccess.Repository.IRepository;
using StoreMaster.Models;
using StoreMaster.Utility;
using Stripe.Checkout;

namespace StoreMaster.Pages.ShoppingCarts
{
    public class OrderConfirmationModel (IUnitOfWork unitOfWork) : PageModel
	{
		private IUnitOfWork unitOfWork = unitOfWork;
		public void OnGet(int headerId)
        {
			OrderHeader orderHeader = unitOfWork.OrderHeader.GetValue(u => u.Id == headerId);

				var service = new SessionService();
				Session session = service.Get(orderHeader.SessionId);

				if (session.PaymentStatus.ToLower() == "paid")
				{
					unitOfWork.OrderHeader.UpdateStripePaymentID(headerId, session.Id, session.PaymentIntentId);
					unitOfWork.OrderHeader.UpdateStatus(headerId, OrderStatus.StatusApproved, PaymentStatus.PaymentStatusApproved);
			}
			HttpContext.Session.Clear();

			List<Cart> shoppingCarts = unitOfWork.ShoppingCart
		   .GetAll(u => u.UserId == orderHeader.UserId).ToList();

			unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
		}
	 }
}
 
