using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolsSchool.DB;
using ToolsSchool.Entities;

namespace ToolsSchool.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SchoolDB _context;

        public ProductsController(SchoolDB context)
        {
            _context = context;
        }

        // GET: Products/Index
        public async Task<IActionResult> Index(string category = null, string search = null,
                                               int page = 1, int pageSize = 12)
        {
            IQueryable<Product> query = _context.Products
                .Where(p => 
                           (!p.ExpiryDate.HasValue || p.ExpiryDate.Value > System.DateTime.Now))
                .OrderByDescending(p => p.CreatedDate);

            // فیلتر بر اساس دسته‌بندی
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
                ViewBag.SelectedCategory = category;
            }

            // جستجو
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.ProductName.Contains(search) ||
                                        p.Description.Contains(search));
                ViewBag.SearchQuery = search;
            }

            // محاسبه اطلاعات صفحه‌بندی
            var totalItems = await query.CountAsync();
            var totalPages = (int)System.Math.Ceiling((double)totalItems / pageSize);

            // گرفتن آیتم‌های صفحه جاری
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // ارسال اطلاعات به View
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            // گرفتن لیست دسته‌بندی‌ها برای فیلتر
            ViewBag.Categories = await _context.Products
                .Select(p => p.Category)
                .Distinct()
                .ToListAsync();

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id );

            if (product == null)
            {
                return NotFound();
            }

            // افزایش تعداد بازدید
            product.ViewCount++;
            _context.Update(product);
            await _context.SaveChangesAsync();

            // گرفتن محصولات مرتبط
            var relatedProducts = await _context.Products
                .Where(p => p.Category == product.Category &&
                           p.Id != product.Id )
                .OrderByDescending(p => p.CreatedDate)
                .Take(4)
                .ToListAsync();

            ViewBag.RelatedProducts = relatedProducts;

            return View(product);
        }

        // GET: Products/ByCategory/{category}
        public async Task<IActionResult> ByCategory(string category, int page = 1)
        {
            if (string.IsNullOrEmpty(category))
            {
                return RedirectToAction("Index");
            }

            var products = await _context.Products
                .Where(p => p.Category == category &&
                           (!p.ExpiryDate.HasValue || p.ExpiryDate.Value > System.DateTime.Now))
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();

            ViewBag.CategoryName = category;
            ViewBag.CurrentPage = page;

            return View("Index", products);
        }

        // GET: Products/Search
        public async Task<IActionResult> Search(string query, int page = 1)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("Index");
            }

            var products = await _context.Products
                .Where(p => (p.ProductName.Contains(query) ||
                            p.Description.Contains(query)) &&
                           (!p.ExpiryDate.HasValue || p.ExpiryDate.Value > System.DateTime.Now))
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();

            ViewBag.SearchQuery = query;
            ViewBag.CurrentPage = page;

            return View("Index", products);
        }
    }
}

