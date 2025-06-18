using HotelBooking.Models;
using HotelBooking.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace HotelBooking.Controllers
{
    public class CategoryController : Controller
    {   
        private readonly DataContext _dataContext;

        public CategoryController(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }
		public async Task<IActionResult> Index(string Slug = "")
		{
			// Tìm kiếm Category dựa trên Slug
			CategoryModel category = _dataContext.Categories
				.Where(c => c.Slug == Slug)
				.FirstOrDefault();

			// Nếu không tìm thấy Category, chuyển hướng về trang Index
			if (category == null) return RedirectToAction("Index");

			// Lấy danh sách sản phẩm theo CategoryId và sắp xếp giảm dần theo Id
			var productsByCategory = _dataContext.Products
				.Where(p => p.CategoryId == category.Id)  // Lấy danh sách sản phẩm
				.OrderByDescending(p => p.Id);             // Sắp xếp theo Id giảm dần

			// Trả về view với danh sách sản phẩm
			return View(await productsByCategory.ToListAsync());
		}
	}
}
