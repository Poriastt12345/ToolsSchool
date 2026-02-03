using Microsoft.AspNetCore.Mvc;

namespace ToolsSchool.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View("../About/Index");
        }
    }
}
