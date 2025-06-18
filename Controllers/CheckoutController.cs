using HotelBooking.Areas.Admin.Repository;
using HotelBooking.Models;
using HotelBooking.Repository;
using HotelBooking.Repository.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HotelBooking.Controllers
{
	public class CheckoutController : Controller
	{	
		private readonly DataContext _dataContext;
		private readonly IEmailSender _emailSender;

		public CheckoutController(DataContext dataContext, IEmailSender emailSender)
		{
			_dataContext = dataContext;
			_emailSender = emailSender;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> Checkout()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if (userEmail == null) 
			{
				return RedirectToAction("Login","Account");
			}
			else
			{
				var orderCode = Guid.NewGuid().ToString();
				var orderItem = new OrderModel();
				orderItem.OrderCode = orderCode;
				orderItem.UserName = userEmail;
				orderItem.Status = 1;
				orderItem.DateTime = DateTime.Now;
				_dataContext.Add(orderItem);
				await _dataContext.SaveChangesAsync();
				List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
				foreach (var cartItem in cartItems) 
				{
					var orderDetails = new OrderDetails();
					orderDetails.UserName = userEmail;
					orderDetails.OrderCode = orderCode;
					orderDetails.ProductId = cartItem.ProductId;
					orderDetails.Price = cartItem.Price;
					orderDetails.Quantity = cartItem.Quantity;
					_dataContext.Add(orderDetails);
					_dataContext.SaveChangesAsync();
				}
				HttpContext.Session.Remove("Cart");
				//send mail
				var receiver = "anhhoangtitan2k4@gmail.com";
				var subject = "Đặt phòng khách sạn trên thiết bị thành công";
				var message = "Đặt phòng khách sạn thành công, mời các bạn trải nghiệm dịch vụ nhé!";
				await _emailSender.SendEmailAsync(receiver, subject, message);
				TempData["success"] = "Check out thành công vui lòng chờ đơn hàng";
				return RedirectToAction("Index", "Cart");
			}
			return View();
		}
	}
}
