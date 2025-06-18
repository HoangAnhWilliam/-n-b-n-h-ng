using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelBooking.Repository
{
	public class SeedData
	{
		public static void SeedingData(DataContext _context)
		{
			// Apply any pending migrations
			_context.Database.Migrate();

			// Declare the variables outside of the if block
			CategoryModel phongdon;
			CategoryModel phongdoi;
			BrandModel muongthanh;
			BrandModel hagl;

			// Seed data if the Categories table is empty
			if (!_context.Categories.Any())
			{
				phongdon = new CategoryModel { Name = "Phongdon", Slug = "phongdon", Description = "Phong don chi co 1 giuong", Status = 1 };
				phongdoi = new CategoryModel { Name = "Phongdoi", Slug = "phongdoi", Description = "Phong doi chi co 2 giuong", Status = 1 };
				muongthanh = new BrandModel { Name = "Muongthanh", Slug = "muongthanh", Description = "Muong Thanh la khach san lon nhat Viet Nam", Status = 1 };
				hagl = new BrandModel { Name = "Hagl", Slug = "hagl", Description = "Hagl la khach san lon nhat Gia Lai", Status = 1 };

				// Add categories and brands to the database
				_context.Categories.AddRange(phongdon, phongdoi);
				_context.Brands.AddRange(muongthanh, hagl);
			}
			else
			{
				// Fetch the existing records from the database if already seeded
				phongdon = _context.Categories.FirstOrDefault(c => c.Slug == "phongdon");
				phongdoi = _context.Categories.FirstOrDefault(c => c.Slug == "phongdoi");
				muongthanh = _context.Brands.FirstOrDefault(b => b.Slug == "muongthanh");
				hagl = _context.Brands.FirstOrDefault(b => b.Slug == "hagl");
			}

			// Now seed the Product data
			if (!_context.Products.Any())
			{
				_context.Products.AddRange(
					new ProductModel { Name = "Phongdon", slug = "phongdon", Description = "Phong don chi co mot giuong", Image = "1.jpg", Category = phongdon, Brand = muongthanh, Price = 12.2M },
					new ProductModel { Name = "Phongdoi", slug = "phongdoi", Description = "Phong doi chi co hai giuong", Image = "2.jpg", Category = phongdoi, Brand = hagl, Price = 12.2M }
				);

				// Save the changes to the database
				_context.SaveChanges();
			}
		}
	}
}
