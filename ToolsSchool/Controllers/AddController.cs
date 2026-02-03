using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToolsSchool.DB;
using ToolsSchool.Entities;
using ToolsSchool.Models;

namespace ToolsSchool.Controllers
{
    public class AddController : Controller
    {
        private readonly SchoolDB _context;
        private readonly IWebHostEnvironment _environment;

        public AddController(SchoolDB context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Add/Create - نمایش فرم ثبت آگهی
        public IActionResult Create()
        {
            return View(); // این فایل باید در Views/Add/Create.cshtml باشد
        }

        // POST: Add/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(AdCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string imagePath = null;
                    string imageFileName = null;
                    long? imageSize = null;
                    string imageContentType = null;

                    // ذخیره عکس در سرور
                    if (model.ProductImage != null && model.ProductImage.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "ads");

                        // ایجاد پوشه اگر وجود نداشت
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        // تولید نام فایل یکتا
                        imageFileName = $"{Guid.NewGuid()}_{Path.GetFileName(model.ProductImage.FileName)}";
                        var filePath = Path.Combine(uploadsFolder, imageFileName);

                        // ذخیره فایل
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ProductImage.CopyToAsync(fileStream);
                        }

                        imagePath = $"/uploads/ads/{imageFileName}";
                        imageSize = model.ProductImage.Length;
                        imageContentType = model.ProductImage.ContentType;
                    }
                    else
                    {
                        ModelState.AddModelError("ProductImage", "عکس محصول الزامی است");
                        return View("Create", model); // بازگشت به فرم Create
                    }

                    // ایجاد شیء Product از ViewModel
                    var product = new Product
                    {
                        ProductName = model.ProductName,
                        Category = model.Category,
                        IsFree = model.IsFree,
                        Price = model.IsFree ? null : model.Price,
                        UsageCondition = model.UsageCondition,
                        PhoneNumber = model.PhoneNumber,
                        Description = model.Description,
                        ImagePath = imagePath,
                        ImageFileName = imageFileName,
                        ImageSize = imageSize,
                        ImageContentType = imageContentType,
                        Status = "Pending",
                        CreatedDate = DateTime.Now,
                        ExpiryDate = DateTime.Now.AddDays(30),
                        ViewCount = 0
                    };

                    // ذخیره در دیتابیس
                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();

                    // نمایش پیام موفقیت
                    TempData["SuccessMessage"] = $"آگهی با موفقیت ثبت شد! کد آگهی: {product.Id}";

                    return View("../Home/Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"خطا در ثبت آگهی: {ex.Message}");
                    Debug.WriteLine(ex);
                }
            }

            // اگر مدل معتبر نبود، بازگشت به فرم با خطاها
            return View("Create", model);
        }

        // GET: Add/Details/5 - نمایش جزئیات آگهی
        public async Task<IActionResult> Details()
        {
           

            return View("Products/Index");
        }

        // GET: Add/Index - لیست آگهی‌ها
        public async Task<IActionResult> Index(string category = null, string search = null)
        {
            IQueryable<Product> query = _context.Products
                .Where(a => a.Status == "Approved" && (!a.ExpiryDate.HasValue || a.ExpiryDate.Value > DateTime.Now))
                .OrderByDescending(a => a.CreatedDate);

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(a => a.Category == category);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.ProductName.Contains(search) ||
                                        a.Description.Contains(search));
            }

            var products = await query.ToListAsync();

            ViewBag.Categories = await _context.Products
                .Where(a => a.Status == "Approved")
                .Select(a => a.Category)
                .Distinct()
                .ToListAsync();

            return View(products);
        }

        // سایر متدها بدون تغییر...
    }
}