using HotelBooking.Models;
using HotelBooking.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
	public class AccountController : Controller
	{
		private UserManager<AppUserModel> _userManager;
		private SignInManager<AppUserModel> _signInManager;

		public AccountController(UserManager<AppUserModel> userManager, SignInManager<AppUserModel> signInManager) 
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		public IActionResult Login(string returnUrl)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl});
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
				if (result.Succeeded)
				{
					return Redirect(loginViewModel.ReturnUrl ?? "/");
				}
				ModelState.AddModelError("", "Invalid Username and Password");
			}
			return View(loginViewModel);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(UserModel user)
		{
			if (ModelState.IsValid) 
			{
				AppUserModel model = new AppUserModel { UserName = user.UserName, Email = user.Email};
				IdentityResult result = await _userManager.CreateAsync(model, user.Password);
				if (result.Succeeded) 
				{
					TempData["success"] = "tạo user thành công";
					return Redirect("/account/login");
				}
				foreach (IdentityError error in result.Errors) 
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View(user);
		}

		public async Task<IActionResult> Logout(string returnUrl = "/")
		{
			await _signInManager.SignOutAsync();
			return Redirect(returnUrl);
		}
	}
}
