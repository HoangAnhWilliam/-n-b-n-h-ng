using HotelBooking.Areas.Admin.Repository;
using HotelBooking.Models;
using HotelBooking.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectedDb"));
            });
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.IsEssential = true;
            });

			builder.Services.AddIdentity<AppUserModel, IdentityRole>()
	.AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
		
			builder.Services.Configure<IdentityOptions>(options =>
			{
				// Password settings.
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 4;

				// User settings.
				options.User.RequireUniqueEmail = true;
			});

			var app = builder.Build();

            app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode={0}");

            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

            app.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "category",
                pattern: "/category/{Slug?}",
                defaults : new { controller = "Category", action = "Index" });

            app.MapControllerRoute(
               name: "brand",
               pattern: "/brand/{Slug?}",
               defaults: new { controller = "Brand", action = "Index" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            //seedingdata
            var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
            SeedData.SeedingData(context); 

            app.Run();
        }
    }
}