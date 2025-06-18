using HotelBooking.Models;
using HotelBooking.Models.ViewModels;
using HotelBooking.Repository;
using HotelBooking.Repository.Components;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _dataContext;
        public CartController(DataContext _context)
        {
            _dataContext = _context;
        }
        public IActionResult Index()
        {   
            List<CartItemModel> cartItems = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemViewModel cartItemViewModel = new()
            {
                CartItems = cartItems,
                GrandTotal = cartItems.Sum(x => x.Quantity * x.Price)
            };
            return View(cartItemViewModel);
        }

		public IActionResult Checkout()
		{
			return View("~/Views/Checkout/Index.cshtml");
		}

        public async Task<IActionResult> Add (long Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart") ?? new List<CartItemModel>();
            CartItemModel cartItem = cart.Where(c => c.ProductId == Id).FirstOrDefault();

            if (cartItem == null) 
            {
                cart.Add(new CartItemModel(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }
            HttpContext.Session.SetJson("Cart", cart);

            TempData["success"] = "Add item to cart successfully";
			return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Decrease(long id)
        {
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
            CartItemModel cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1) 
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }

            if(cart.Count == 0)
            {
				HttpContext.Session.Remove("Cart");
			}
            else
            {
				HttpContext.Session.SetJson("Cart", cart);
			}
            TempData["success"] = "Decrease item quantity to cart successfully";
            return RedirectToAction("Index");
        }

		public async Task<IActionResult> Increase(long id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			CartItemModel cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

			if (cartItem.Quantity >= 1)
			{
				++cartItem.Quantity;
			}
			else
			{
				cart.RemoveAll(p => p.ProductId == id);
			}

			if (cart.Count == 0)
			{
				HttpContext.Session.Remove("Cart");
			}
			else
			{
				HttpContext.Session.SetJson("Cart", cart);
			}
            TempData["success"] = "Increase item quantity to cart successfully";
            return RedirectToAction("Index");
		}

		public async Task<IActionResult> Remove(long id)
		{
			List<CartItemModel> cart = HttpContext.Session.GetJson<List<CartItemModel>>("Cart");
			cart.RemoveAll(p => p.ProductId == id);

            if (cart.Count == 0) 
            {
				HttpContext.Session.Remove("Cart");
			}
            else
            {
				HttpContext.Session.SetJson("Cart", cart);
			}
            TempData["success"] = "Remove item of cart successfully";
            return RedirectToAction("Index");
		}

        public async Task<IActionResult> Clear(long id)
        {
			HttpContext.Session.Remove("Cart");
            TempData["success"] = "Clear all item of cart successfully";
            return RedirectToAction("Index");
		}
	}
}
