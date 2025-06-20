using HotelBooking.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Slider")]
    //[Authorize(Roles = "Admin,Author,Publisher")]
    public class SliderController : Controller
    {
        private readonly DataContext _dataContext;

        public SliderController(DataContext context)
        {
            _dataContext = context;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Sliders.OrderByDescending(p => p.Id).ToListAsync());
        }
    }
}
