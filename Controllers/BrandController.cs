using HotelBooking.Models;
using HotelBooking.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Controllers
{
	public class BrandController : Controller
	{
		private readonly DataContext _dataContext;
		public BrandController(DataContext context)
		{
			_dataContext = context;
		}
		
		public async Task<IActionResult> Index(string Slug = "")
		{
			// Tìm kiếm Category dựa trên Slug
			BrandModel brand = _dataContext.Brands
				.Where(c => c.Slug == Slug)
				.FirstOrDefault();

			// Nếu không tìm thấy Category, chuyển hướng về trang Index
			if (brand == null) return RedirectToAction("Index");

			// Lấy danh sách sản phẩm theo CategoryId và sắp xếp giảm dần theo Id
			var productsByBrand = _dataContext.Products
				.Where(p => p.BrandId == brand.Id)  // Lấy danh sách sản phẩm
				.OrderByDescending(p => p.Id);             // Sắp xếp theo Id giảm dần

			// Trả về view với danh sách sản phẩm
			return View(await productsByBrand.ToListAsync());
		}
	}
}
