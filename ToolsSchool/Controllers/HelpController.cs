using Microsoft.AspNetCore.Mvc;

namespace ToolsSchool.Controllers
{
    public class HelpController : Controller
    {
        public IActionResult Index()
        {
            return View("../Help/Index");
        }
    }
}
