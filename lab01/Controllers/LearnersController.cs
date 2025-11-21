using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab01.Data;
using lab01.Models;

namespace lab01.Controllers
{
    public class LearnersController : Controller
    {
        private SchoolContext db; // [cite: 444]
        private int pageSize = 3;

        public LearnersController(SchoolContext context)
        {
            db = context; // [cite: 447]
        }

        // 8.1 List (R) – Hiển thị danh sách
        //public IActionResult Index()
        //{
        //    // Dùng Include để load thông tin Major liên quan (Eager Loading)
        //    var learners = db.Learners.Include(m => m.Major).ToList(); // [cite: 451]
        //    return View(learners);
        //}
        // [Sửa Action Index trong LearnerController.cs]
        public IActionResult Index(int? mid) 
        {
            var learners = (IQueryable<Learner>)db.Learners
        .Include(m => m.Major);

            if (mid != null)
            {
                learners = (IQueryable<Learner>)db.Learners
                    .Where(l => l.MajorID == mid)
                    .Include(m => m.Major);
            }
            // Tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize); 
            // Trả số trang về view để hiển thị nav-trang
            ViewBag.pageNum = pageNum;
            // Lấy dữ liệu trang đầu
            var result = learners.Take(pageSize).ToList();
            return View(result);
        }
        //cach 2 : Tạo Action mới để lọc Learner theo MajorID và trả về PartialView
        public IActionResult LearnerByMajorID(int mid)
        {
            // Lọc Learner theo MajorID
            var learners = db.Learners
                .Where(l => l.MajorID == mid) // [cite: 200]
                .Include(m => m.Major).ToList(); // [cite: 200]

            // Trả về một PartialView tên là "LearnerTable"
            return PartialView("LearnerTable", learners); // [cite: 200]
        }

        public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
        {
            // Lấy toàn bộ learners
            var learners = (IQueryable<Learner>)db.Learners;

            // Lấy chỉ số trang, nếu null hoặc <= 0 thì mặc định bằng 1
            int page = (int)(pageIndex == null || pageIndex <= 0 ? 1 : pageIndex); 

            // 1. Lọc theo MajorID (mid)
             if (mid != null) 
            {
                // Lọc
                learners = learners.Where(l => l.MajorID == mid);
                                                                  // Gửi mid về view
                ViewBag.mid = mid;
            }

            // 2. Tìm kiếm theo keyword (tên)
             if (keyword != null) 
            {
                // Tìm kiếm (chuyển về chữ thường để tìm kiếm không phân biệt hoa thường)
                learners = learners.Where(l => l.FirstMidName.ToLower().Contains(keyword.ToLower())); 
                                                                                                      // Gửi keyword về view
                ViewBag.keyword = keyword; 
            }

            // 3. Tính toán và Phân trang
            // Tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize); 
                                                                                 // Gửi số trang về view
            ViewBag.pageNum = pageNum; 

            // Chọn dữ liệu trong trang hiện tại: Bỏ qua (Skip) các phần tử của các trang trước, rồi Lấy (Take) số lượng phần tử của trang hiện tại.
             var result = learners.Skip(pageSize * (page - 1))
                                 .Take(pageSize)
                                 .Include(m => m.Major) 
                                 .ToList(); // Cần thêm .ToList() nếu `result` không cần là IQueryable nữa.

            // Trả về Partial View chỉ chứa bảng dữ liệu và thanh phân trang
            return PartialView("LearnerTable", result);
        }

        // 8.2 Create (C) – Thêm mới

        [HttpGet] 
        public IActionResult Create()
        {
            // Gửi danh sách Majors về View để làm dropdown list
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName"); // [cite: 556]
            return View();
        }

        
        [HttpPost] // [cite: 558]
        
        [ValidateAntiForgeryToken] // [cite: 559]
        // Sửa lỗi [Bind]: "Enrollment Date" -> "EnrollmentDate"
        
        public IActionResult Create([Bind("FirstMidName, LastName, MajorID, EnrollmentDate")] Learner learner) // [cite: 560-561]
        {
             if (ModelState.IsValid) // [cite: 563]
            {
                db.Learners.Add(learner); // [cite: 565]
                db.SaveChanges(); // [cite: 566]
                return RedirectToAction(nameof(Index)); // [cite: 567]
            }

            // SỬA LỖI PDF:
            // Nếu ModelState không hợp lệ, phải tải lại ViewBag
            // Code này trong PDF bị đặt sai bên ngoài method [cite: 570-572]
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View(learner); // Trả về view với dữ liệu đã nhập
        }

        // 8.3 Update (U) – Sửa
        
        [HttpGet] // [cite: 736]
        public IActionResult Edit(int id)
        {
             if (id == null || db.Learners == null) // [cite: 740]
            {
                return NotFound(); // [cite: 742]
            }
            var learner = db.Learners.Find(id); // [cite: 744]
            if (learner == null)
            {
                return NotFound(); // [cite: 747]
            }

            // Gửi SelectList với MajorID của learner này đang được chọn
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID); // [cite: 750]
            return View(learner);
        }

        
        [HttpPost] // [cite: 751]
        
        [ValidateAntiForgeryToken] // [cite: 752]
        // Sửa lỗi [Bind]: "Learner ID" -> "LearnerID", "Enrollment Date" -> "EnrollmentDate"
        
        public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner) // [cite: 753-754]
        {
             if (id != learner.LearnerID) // [cite: 756]
            {
                return NotFound(); // [cite: 758]
            }

             if (ModelState.IsValid) // [cite: 760]
            {
                try
                {
                    db.Update(learner); // [cite: 764]
                    db.SaveChanges(); // [cite: 765]
                }
                catch (DbUpdateConcurrencyException)
                {
                     if (!LearnerExists(learner.LearnerID)) // [cite: 768]
                    {
                        return NotFound(); // [cite: 770]
                    }
                    else
                    {
                        throw; // [cite: 772]
                    }
                }
                return RedirectToAction(nameof(Index)); // [cite: 776]
            }

            // Nếu không valid, tải lại ViewBag
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID); // [cite: 777]
            return View(learner);
        }

        // 8.4 Delete (D) – Xóa
        
        [HttpGet] // [cite: 905]
        public IActionResult Delete(int id)
        {
             if (id == null || db.Learners == null) // [cite: 906]
            {
                return NotFound(); // [cite: 910]
            }

            // Load learner, Major, và các Enrollments liên quan
             var learner = db.Learners.Include(l => l.Major) // [cite: 912]
                                    .Include(e => e.Enrollments) // [cite: 913]
                                    .FirstOrDefault(m => m.LearnerID == id); // [cite: 914]

            if (learner == null)
            {
                return NotFound(); // [cite: 918]
            }

            // KIỂM TRA RÀNG BUỘC: Nếu learner còn Enrollment, không cho xóa
             if (learner.Enrollments.Count() > 0) // [cite: 919]
            {
                return Content("This learner has some enrollments, can't delete!"); // [cite: 920]
            }

            return View(learner); // [cite: 923]
        }

        
        [HttpPost, ActionName("Delete")] // [cite: 926]
        
        [ValidateAntiForgeryToken] // [cite: 927]
        public IActionResult DeleteConfirmed(int id)
        {
             if (db.Learners == null) // [cite: 930]
            {
                return Problem("Entity set 'Learners' is null."); // [cite: 932]
            }

            var learner = db.Learners.Find(id); // [cite: 934]
            if (learner != null)
            {
                db.Learners.Remove(learner); // [cite: 937]
            }

            db.SaveChanges(); // [cite: 939]
            return RedirectToAction(nameof(Index)); // [cite: 940]
        }

        // Hàm hỗ trợ kiểm tra Learner tồn tại
        private bool LearnerExists(int id)
        {
            return (db.Learners?.Any(e => e.LearnerID == id)).GetValueOrDefault(); // [cite: 781]
        }
    }
}
