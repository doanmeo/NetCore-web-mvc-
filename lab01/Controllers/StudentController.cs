using lab01.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace lab01.Controllers
{
    public class StudentController : Controller
    {
        // 1. SỬA LỖI: Chuyển thành 'static' để danh sách được lưu trữ qua các request
        private static List<Student> listStudents = new List<Student>();
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;

            // 2. SỬA LỖI: Chỉ khởi tạo danh sách nếu nó đang rỗng
            if (!listStudents.Any())
            {
                listStudents = new List<Student>()
                {
                    new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT,
                                    Gender = Gender.Male, IsRegular=true,
                                    Address = "A1-2018", Email = "nam@g.com" },
                    new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE,
                                    Gender = Gender.Female, IsRegular=true,
                                    Address = "A1-2019", Email = "tu@g.com" },
                    new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE,
                                    Gender = Gender.Male, IsRegular=false,
                                    Address = "A1-2020", Email = "phong@g.com" },
                    new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE,
                                    Gender = Gender.Female, IsRegular=false,
                                    Address = "A1-2021", Email = "mai@g.com" }
                };
            }
        }

        public ActionResult Index()
        {
            // Trả về View Index.cshtml cùng Model là danh sách sv listStudents
            return View(listStudents);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //lấy danh sách các giá trị Gender để hiển thị radio button trên form 
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();

            //lấy danh sách các giá trị Branch để hiển thị select-option trên form
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };

            return View();
        }

        [HttpPost]
        public IActionResult Create(Student s)
        {
            // 3. SỬA LỖI: Thêm kiểm tra validation (yêu cầu của Lab 3)
            if (ModelState.IsValid)
            {
                // Xử lý upload file nếu có
                if (s.AvatarFile != null)
                {
                    // 1. Tạo đường dẫn đến thư mục wwwroot/images/avatars
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/avatars");
                    if (!Directory.Exists(uploadsFolder)) // Tạo thư mục nếu chưa có
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // 2. Tạo tên file độc nhất để tránh bị ghi đè
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + s.AvatarFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // 3. Lưu file vào đường dẫn
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        s.AvatarFile.CopyTo(fileStream);
                    }

                    // 4. Lưu đường dẫn (tương đối) vào model
                    s.AvatarUrl = "/images/avatars/" + uniqueFileName;
                }

                // Nếu hợp lệ, thêm sinh viên vào danh sách
                s.Id = listStudents.Last<Student>().Id + 1;
                listStudents.Add(s);

                // 4. SỬA LỖI: Dùng RedirectToAction (PRG Pattern)
                return RedirectToAction("Index");
            }

            // 5. SỬA LỖI: Nếu ModelState *KHÔNG* hợp lệ,
            // Cần tải lại ViewBag để hiển thị lại form với các dropdown list
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.AllBranches = new List<SelectListItem>()
            {
                new SelectListItem { Text = "IT", Value = "1" },
                new SelectListItem { Text = "BE", Value = "2" },
                new SelectListItem { Text = "CE", Value = "3" },
                new SelectListItem { Text = "EE", Value = "4" }
            };

            // Trả về View() với model 's' (chứa dữ liệu người dùng đã nhập và thông báo lỗi)
            return View(s);
        }
    }
}