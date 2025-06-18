using Microsoft.AspNetCore.Identity;

namespace HotelBooking.Models
{
	public class AppUserModel : IdentityUser
	{
		public string RoleId { get; set; }
	}
}
