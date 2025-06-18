namespace HotelBooking.Models
{
	public class OrderModel
	{
		public int Id { get; set; }

		public string OrderCode { get; set; }

		public string UserName { get; set; }

		public DateTime DateTime { get; set; }

		public int Status { get; set; }
	}
}
