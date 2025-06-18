using HotelBooking.Models.Validation;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelBooking.Models
{
    public class ProductModel
    {
        [Key]

        public long Id { get; set; }

		[Required(ErrorMessage = "Yêu cầu nhập tên sản phẩm")]

		public string Name { get; set; }

		public string slug { get; set; }

		[Required, MinLength(4, ErrorMessage = "Yêu cầu nhập mô tả sản phẩm")]

		public string Description { get; set; }

		[Required(ErrorMessage = "Yêu cầu nhập giá danh mục")]
		[Range(0.01, double.MaxValue)]
		[Column(TypeName = "decimal(18, 2)")]

		public decimal Price { get; set; }

		[Required, Range(1, int.MaxValue, ErrorMessage ="Chọn một thương hiệu")]

		public int BrandId { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "Chọn một danh mục")]

        public int CategoryId { get; set; }

        public CategoryModel Category { get; set; }

        public BrandModel Brand { get; set; }

		public string Image { get; set; }

		[NotMapped]
		[FileExtension]
		public IFormFile? ImageUpload { get; set; }
    }
}
