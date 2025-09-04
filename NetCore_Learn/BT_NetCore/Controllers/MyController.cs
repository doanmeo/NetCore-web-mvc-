using Microsoft.AspNetCore.Mvc;

namespace BT_NetCore.Controllers
{
    public class MyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyItem()
        {
            return View();
        }
    }
}
