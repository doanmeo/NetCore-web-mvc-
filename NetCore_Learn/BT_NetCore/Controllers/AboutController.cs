using Microsoft.AspNetCore.Mvc;

namespace BT_NetCore.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
