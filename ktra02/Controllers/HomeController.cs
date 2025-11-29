using System.Diagnostics;
using ktra02.Models;
using Microsoft.AspNetCore.Mvc;

namespace ktra02.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QlhangHoaContext _context;


        public HomeController(ILogger<HomeController> logger, QlhangHoaContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var hanghoa = _context.HangHoas.Where(p => p.Gia >= 100).ToList();
            return View("vvd_maincontent",hanghoa);
        }
        public IActionResult GetProductsByLoai(int maLoai)
        {
            var products = _context.HangHoas.Where(p => p.MaLoai == maLoai).ToList();
            // SỬA DÒNG NÀY: Trả về _ProductList thay vì vvd_maincontent
            return PartialView("_ProductList", products);
        }

        // 1. Action GET: Hiển thị form thêm mới
        [HttpGet]
        public IActionResult Create()
        {
            // Lấy danh sách Loại hàng đưa vào ViewBag để hiển thị Dropdown
            ViewBag.MaLoai = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.LoaiHangs, "MaLoai", "TenLoai");
            return View();
        }

        // 2. Action POST: Xử lý khi người dùng nhấn nút Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public IActionResult Create(HangHoa hangHoa)
        // 1. Đổi kiểu trả về thành async Task<IActionResult>
        public async Task<IActionResult> Create(HangHoa hangHoa)
        {
            // 1. Loại bỏ check null cho bảng phụ (Loại hàng) vì ta chỉ cần MaLoai
            ModelState.Remove("MaLoaiNavigation");
            // Kiểm tra xem dữ liệu có thỏa mãn các Annotation ở Bước 1 không
            if (ModelState.IsValid)
            {
                try
                {
                    // 2. Thêm vào context
                    _context.Add(hangHoa);

                    // 3. Dùng SaveChangesAsync thay vì SaveChanges
                    await _context.SaveChangesAsync();

                    // 4. Kiểm tra lại tên Action này có đúng không? 
                    // Nếu bạn muốn về trang danh sách, thường là "Index"
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Bắt lỗi nếu có (VD: Lỗi kết nối DB, lỗi khóa ngoại)
                    ModelState.AddModelError("", "Lỗi lưu dữ liệu: " + ex.Message);
                }
            }

            // Nếu không hợp lệ (ví dụ nhập giá 90), thì load lại form và hiển thị lỗi
            // Nhớ load lại Dropdown Loại hàng nếu không sẽ bị lỗi null
            ViewBag.MaLoai = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.LoaiHangs, "MaLoai", "TenLoai", hangHoa.MaLoai);
            return View(hangHoa);
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
