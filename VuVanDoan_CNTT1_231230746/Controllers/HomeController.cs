using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VuVanDoan_CNTT1_231230746.String;

namespace VuVanDoan_CNTT1_231230746.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VanTai2512V1Context _context;

        public HomeController(ILogger<HomeController> logger, VanTai2512V1Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ////var products = _context.Chuyens.Where(p => p.SoXe >= 100).ToList();
            //var products = _context.Chuyens
            //                       .OrderByDescending(p => p.SoXe)
            //                       .Take(6)                       
            //                       .ToList();
            //var anh = _context.Xes.OrderByDescending(p => p.SoXe)
            //                       .Take(6)
            //                       .ToList();
            //var tuyen = _context.Tuyens
            //                       .Take(6)
            //                       .ToList();
            return View("VuVanDoan_MainContent");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
