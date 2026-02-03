using Microsoft.AspNetCore.Mvc;

namespace ToolsSchool.Controllers
{
    public class MyadsController : Controller
    {
        public IActionResult Index()
        {
            return View("../Myads/Index");
        }
    }
}
