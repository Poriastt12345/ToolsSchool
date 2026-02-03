using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToolsSchool.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام محصول الزامی است")]
        [Display(Name = "نام محصول")]
        [StringLength(200, ErrorMessage = "نام محصول نمی‌تواند بیشتر از 200 کاراکتر باشد")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "دسته‌بندی الزامی است")]
        [Display(Name = "دسته‌بندی")]
        [StringLength(100, ErrorMessage = "دسته‌بندی نمی‌تواند بیشتر از 100 کاراکتر باشد")]
        public string Category { get; set; }

        [Display(Name = "رایگان است؟")]
        public bool IsFree { get; set; } = false;

        [Display(Name = "قیمت")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "قیمت باید مثبت باشد")]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "وضعیت کالا الزامی است")]
        [Display(Name = "کارکرد")]
        [StringLength(50)]
        public string UsageCondition { get; set; } = "New";

        [Required(ErrorMessage = "شماره تلفن الزامی است")]
        [Phone(ErrorMessage = "شماره تلفن معتبر نیست")]
        [Display(Name = "شماره تلفن")]
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Display(Name = "توضیحات")]
        [StringLength(1000, ErrorMessage = "توضیحات نمی‌تواند بیشتر از 1000 کاراکتر باشد")]
        public string? Description { get; set; }

        [Display(Name = "مسیر عکس")]
        [StringLength(500)]
        public string ImagePath { get; set; }

        [Display(Name = "نام فایل عکس")]
        [StringLength(255)]
        public string ImageFileName { get; set; }

        [Display(Name = "سایز عکس (بایت)")]
        public long? ImageSize { get; set; }

        [Display(Name = "نوع فایل عکس")]
        [StringLength(100)]
        public string ImageContentType { get; set; }

        [Display(Name = "وضعیت آگهی")]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Sold, Expired

        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "تاریخ به‌روزرسانی")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "تاریخ انقضا")]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "تعداد بازدید")]
        public int ViewCount { get; set; } = 0;

        // Foreign Key برای کاربر (اگر سیستم کاربری دارید)
        [Display(Name = "ایجاد کننده")]
        public string? CreatedByUserId { get; set; }

        [Display(Name = "ایجاد کننده")]
        [StringLength(100)]
        public string? CreatedByUserName { get; set; }

        // اطلاعات تماس اضافی
        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "ایمیل معتبر نیست")]
        [StringLength(100)]
        public string? Email { get; set; }

        [Display(Name = "شهر")]
        [StringLength(100)]
        public string? City { get; set; }

        [Display(Name = "آدرس")]
        [StringLength(500)]
        public string? Address { get; set; }

        [Display(Name = "کد پستی")]
        [StringLength(20)]
        public string? PostalCode { get; set; }

        // متدهای کمکی
        public string GetFormattedPrice()
        {
            if (IsFree) return "رایگان";
            if (Price.HasValue) return $"{Price.Value:N0} تومان";
            return "توافقی";
        }

        public string GetStatusColor()
        {
            return Status switch
            {
                "Approved" => "success",
                "Pending" => "warning",
                "Rejected" => "danger",
                "Sold" => "info",
                "Expired" => "secondary",
                _ => "light"
            };
        }

        public bool IsActive()
        {
            return Status == "Approved" && (!ExpiryDate.HasValue || ExpiryDate.Value > DateTime.Now);
        }

    }
}
