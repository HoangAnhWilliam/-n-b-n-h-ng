﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelBooking.Models.Validation;

namespace HotelBooking.Models
{
    public class SliderModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập tên Slider")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập mô tả slider")]
        public string Description { get; set; }

        public int? Status { get; set; }

        public string Image { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
