using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMaster.DataAccess.Repository;
using StoreMaster.DataAccess.Repository.IRepository;
using StoreMaster.Models;
using StoreMaster.Models.ViewModels;
using StoreMaster.Utility;
using Stripe.Checkout;
using Stripe.Climate;
using System.Security.Claims;

namespace StoreMaster.Pages.ShoppingCarts
{
    [BindProperties]
    public class SummaryModel   : PageModel
    {
		private   IUnitOfWork UnitOfWork { set; get; } 
		private   SignInManager<IdentityUser> SignInManager { set; get; }
        public bool isUserAlreadyHadOrder = false;
        public SummaryOrderViewModel SummaryOrderViewModel { get; set; }
        public OrderHeader OrderHeader { get; set; }



        public SummaryModel( IUnitOfWork unitOfWork, SignInManager < IdentityUser > signInManager)  
		{
			this.UnitOfWork = unitOfWork;
			this.SignInManager = signInManager;
			SummaryOrderViewModel = new();
		    OrderHeader = new();
        }

        public void OnGet()
        {

            var userId = SignInManager.UserManager.GetUserId(User);
            var result = UnitOfWork.OrderHeader.GetValue(orderHeaderItem => orderHeaderItem.UserId == userId);
            if (result != null)
            {
                isUserAlreadyHadOrder = true;
                OrderHeader = result;
            }
            SummaryOrderViewModel.Items = UnitOfWork.ShoppingCart.GetAll(c => c.UserId == userId).ToList();

            foreach (var cart in SummaryOrderViewModel.Items)
            {
                SummaryOrderViewModel.TotalPrice += (int)cart.Product.Price * cart.Count;
                SummaryOrderViewModel.TotalNumberOfProducts += cart.Count;
            }

        }

		public IActionResult OnPostPlaceholder() 
		{
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(isUserAlreadyHadOrder);

            var userId = SignInManager.UserManager.GetUserId(User);

			   var items = UnitOfWork.ShoppingCart.GetAll(c => c.UserId == userId).ToList();
               SummaryOrderViewModel.Items = items;

            SummaryOrderViewModel = new()
            {
                Items = items,
                OrderHeader = OrderHeader
            };
			SummaryOrderViewModel.OrderHeader.UserId = userId;
			SummaryOrderViewModel.OrderHeader.OrderDate = System.DateTime.Now;
            

            if (items != null)
            {

                foreach (var cart in items)
                {
                    SummaryOrderViewModel.OrderHeader.OrderTotal += (cart.Price * cart.Count);
                }


				SummaryOrderViewModel.OrderHeader.PaymentStatus = OrderStatus.StatusPending;
				SummaryOrderViewModel.OrderHeader.OrderStatus = PaymentStatus.PaymentStatusPending;

				UnitOfWork.OrderHeader.Add(SummaryOrderViewModel.OrderHeader);


                foreach (var cart in items)
                {
                    OrderDetail orderDetail = new()
                    {
                        ProductId = cart.ProductId,
                        OrderHeaderId = SummaryOrderViewModel.OrderHeader.Id,
                        Price = cart.Price,
                        Count = cart.Count

                    };
                    UnitOfWork.OrderDetail.Add(orderDetail);
                }
                string domine = "https://localhost:44327/";

				var options = new SessionCreateOptions
                {
                 SuccessUrl= domine+ $"ShoppingCarts/OrderConfirmation?headerId={SummaryOrderViewModel.OrderHeader.Id}",
				 CancelUrl = domine + "ShoppingCarts/DisplayShoppingCart",
				 LineItems = new List<SessionLineItemOptions>(),
				 Mode = "payment",
				};


				foreach (var item in items)
				{
					var sessionLineItem = new SessionLineItemOptions
					{
						PriceData = new SessionLineItemPriceDataOptions
						{
							UnitAmount = (long)(item.Product.Price * 100), // $20.50 => 2050 
							Currency = "usd",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = item.Product.Name
							}
						},
						Quantity = item.Count
					};
					options.LineItems.Add(sessionLineItem);
				}

				var service = new SessionService();
				Session session = service.Create(options);

				 UnitOfWork.OrderHeader.UpdateStripePaymentID(SummaryOrderViewModel.OrderHeader.Id, session.Id, session.PaymentIntentId);

				Response.Headers.Add("Location", session.Url);
				return new StatusCodeResult(303);
			}
            else
            {
                return Page();
            }
		}
	}
}
