using HotelBooking.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace HotelBooking.Areas.Admin.Api
{
    [Area("Admin")]
    [Route("Admin/api")]
    public class AdminApiController : ControllerBase
    {
        public readonly DataContext _dataContext;

        public AdminApiController(DataContext dataContext)
        {
            _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        public IActionResult Index()
        {
            return Ok();
        }

        //https://<hostname>/Admin/api/users-with-roles
        [HttpGet("users-with-roles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            // Lấy danh sách user cùng role
            var usersWithRoles = await (from u in _dataContext.Users
                                        join ur in _dataContext.UserRoles on u.Id equals ur.UserId
                                        join r in _dataContext.Roles on ur.RoleId equals r.Id
                                        select new
                                        {
                                            UserId = u.Id,
                                            UserName = u.UserName,
                                            Email = u.Email,
                                            Phone = u.PhoneNumber,
                                            Password= u.PasswordHash,
                                            RoleName = r.Name
                                        }).ToListAsync();

            // Trả về JSON dữ liệu
            return Ok(usersWithRoles); // Trả về HTTP 200 với dữ liệu JSON
        }

        //https://<hostname>/Admin/api/products
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _dataContext.Products.OrderByDescending(p => p.Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync());
        }

        //https://<hostname>/Admin/api/brands
        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            return Ok(await _dataContext.Brands.OrderByDescending(p => p.Id).ToListAsync());
        }

        //https://<hostname>/Admin/api/categories
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _dataContext.Categories.OrderByDescending(p => p.Id).ToListAsync());
        }

        //https://<hostname>/Admin/api/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Tìm người dùng khớp với email và số điện thoại
            var user = await _dataContext.Users.FirstOrDefaultAsync(u =>
                u.Email == loginRequest.Email && u.PhoneNumber == loginRequest.PhoneNumber);

            if (user == null)
            {
                // Trả về lỗi nếu không tìm thấy người dùng
                return Unauthorized(new { message = "Email hoặc số điện thoại không chính xác" });
            }

            // Nếu thành công, trả về thông tin người dùng (có thể thêm token JWT nếu cần)
            return Ok(new
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
        }

        // Lớp nhận yêu cầu đăng nhập
        public class LoginRequest
        {
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
