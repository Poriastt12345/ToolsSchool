using Microsoft.AspNetCore.Mvc;

namespace ToolsSchool.Controllers
{
    public class LikeController : Controller
    {
        public IActionResult Index()
        {
            return View("../Like/Index");
        }
    }
}
