using System.ComponentModel.DataAnnotations;

namespace ToolsSchool.Models
{
    public class AdCreateViewModel
    {
        [Required(ErrorMessage = "نام محصول الزامی است")]
        [Display(Name = "نام محصول")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "دسته‌بندی الزامی است")]
        [Display(Name = "دسته‌بندی")]
        public string Category { get; set; }

        [Display(Name = "رایگان است؟")]
        public bool IsFree { get; set; }

        [Display(Name = "قیمت")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت باید مثبت باشد")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "وضعیت کالا الزامی است")]
        [Display(Name = "کارکرد")]
        public string UsageCondition { get; set; }

        [Required(ErrorMessage = "شماره تلفن الزامی است")]
        [Phone(ErrorMessage = "شماره تلفن معتبر نیست")]
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "عکس محصول")]
        [Required(ErrorMessage = "عکس محصول الزامی است")]
        [DataType(DataType.Upload)]  
        public IFormFile ProductImage { get; set; }
    }
    public enum UsageCondition
    {
        New,
        LikeNew,
        Used
    }
}
