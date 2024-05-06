using StoreMaster.DataAccess.Data;
using StoreMaster.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StoreMaster.DataAccess.Repository.IRepository;
using StoreMaster.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StoreMaster.Pages.Products
{
    [BindProperties]
    public class CreateModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment) : PageModel
    {
        public List<SelectListItem> Categories { get; set; }
        public ProductViewModel Product { get; set; }
        
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

        public void OnGet()
        {
            Categories = unitOfWork.Category.GetAll().Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.CategoryId.ToString(),
                                      Text = a.Name
                                  }).ToList();
        }

        public IActionResult OnPost(IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            if (file != null)
            {

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

                Product product = new Product()
                {
                    CategoryId = Product.CategoryId ,
                    Name = Product.Name,
                    Author = Product.Author,
                    Description = Product.Description,
                    Price = Product.Price,
                    Price50 = Product.Price50,
                    Price100 = Product.Price100,
                    ImageUrl = imageUrl
                };
                try
                {
                    unitOfWork.Product.Add(product);
                    TempData["success"] = file.FileName;
                    return RedirectToPage("DisplayAllProducts");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    return RedirectToPage("Create");
                }
            }
            else
            {
                return Page();
            }
        }

    }
}
