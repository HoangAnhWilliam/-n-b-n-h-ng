using HotelBooking.Models;
using HotelBooking.Models.ViewModels;
using HotelBooking.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Controllers
{
    public class ProductController : Controller
    {   
        private readonly DataContext _dataContext;

        public ProductController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

		public async Task<IActionResult> Details(long Id)
		{
			if (Id == null) return RedirectToAction("Index");

			var productsById = _dataContext.Products
				.Where(p => p.Id == Id)
				.FirstOrDefault();

            var relatedProducts = await _dataContext.Products.Include(p => p.Ratings).Where(p => p.CategoryId == productsById.CategoryId && p.Id != productsById.Id).Take(4).ToListAsync();
            ViewBag.RelatedProducts = relatedProducts;

            var viewModel = new ProductDetailsViewModel
            {
                ProductDetails = productsById,
            };
            return View(viewModel);
		}

        public async Task<IActionResult> CommentProduct(RatingModel rating)
        {
            if (ModelState.IsValid)
            {
                var ratingEntity = new RatingModel
                {
                    ProductId = rating.ProductId,
                    Comment = rating.Comment,
                    Name = rating.Name,
                    Email = rating.Email,
                    Star = rating.Star,
                };

                _dataContext.Ratings.Add(ratingEntity); // Thêm đánh giá vào cơ sở dữ liệu
                await _dataContext.SaveChangesAsync(); // Lưu thay đổi

                TempData["success"] = "Cảm ơn bạn đã đánh giá sản phẩm!";

                return Redirect(Request.Headers["Referer"]); // Quay lại trang trước đó
            }
            else
            {
                TempData["error"] = "Model có một vài thứ đang bị lỗi";
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                string errorMessage = string.Join("\n", errors);
                return RedirectToAction("Details", new {id = rating.ProductId});
            }
            return Redirect(Request.Headers["Referer"]); // Quay lại trang trước đó
        }

        public async Task<IActionResult> Search(string searchTerm)
        {
            var products = await _dataContext.Products.Where(p=>p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm)).ToListAsync();
            ViewBag.Keyword = searchTerm;
            return View(products);
        }
	}
}
