using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models.ViewModels
{
    public class ProductDetailsViewModel
    {
        public ProductModel ProductDetails { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập bình luận sản phẩm")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập tên người dùng")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập email người dùng")]
        public string Email { get; set; }
    }
}
