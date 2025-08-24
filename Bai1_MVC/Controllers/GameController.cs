using Microsoft.AspNetCore.Mvc;

namespace Bai1_MVC.Controllers
{
    public class GameController : Controller
    {
        // Fixed: IDE1006 (method name PascalCase), CA1822 (static), CS0029 (return View)
        public IActionResult show()
        {
            return View();
        }
        public IActionResult anime()
        {
            return View();
        }

    }
}
