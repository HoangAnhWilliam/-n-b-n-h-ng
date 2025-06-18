using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
