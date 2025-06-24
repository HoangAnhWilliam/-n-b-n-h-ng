using HotelBooking.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Contact")]
    public class ContactController : Controller
    {   
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ContactController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            var contact = _dataContext.Contacts.ToList();
            return View(contact);
        }
    }
}
