﻿using HotelBooking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Repository
{
	public class DataContext : IdentityDbContext<AppUserModel>
	{
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<BrandModel> Brands { get; set; }

		public DbSet<SliderModel> Sliders { get; set; }

        public DbSet<ProductModel> Products { get; set; }

		public DbSet<RatingModel> Ratings { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }

		public DbSet<OrderModel> Orders { get; set; }

		public DbSet<OrderDetails> OrderDetails { get; set; }

		public DbSet<ContactModel> Contacts { get; set; }
    }

}
