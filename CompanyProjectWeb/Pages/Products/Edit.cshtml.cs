using StoreMaster.DataAccess.Data;
using StoreMaster.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMaster.DataAccess.Repository.IRepository;
using StoreMaster.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace StoreMaster.Pages.Products
{

    [BindProperties]
    public class EditModel : PageModel
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
    
        public Product Product { get; set; }

        public IUnitOfWork unitOfWork;
        public EditModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            this.unitOfWork = unitOfWork;
        }

        public void OnGet(int ProductId)
        {
            if (ProductId != null && ProductId != 0)
            {
                Product = unitOfWork.Product.GetValue(p => p.ProductId == ProductId);
            }
        }

        public IActionResult OnPost(IFormFile file)
        {

            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = @"images\products";
                    string finalPath = Path.Combine(wwwRootPath, productPath);

                    if (!Directory.Exists(finalPath))
                        Directory.CreateDirectory(finalPath);

                    using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    var imageUrl = @"\" + productPath + @"\" + fileName;
                    Product.ImageUrl = imageUrl;
                    unitOfWork.Product.Update(Product);

                    TempData["success"] = "Product Edited successfully";
                    return RedirectToPage("DisplayAllProducts");
                } 
                unitOfWork.Product.Update(Product);
                TempData["success"] = "Product Edited successfully";
                return RedirectToPage("DisplayAllProducts");
            }

            return Page();
        }

    }
}
