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

        public LearnersController(SchoolContext context)
        {
            db = context; // [cite: 447]
        }

        // 8.1 List (R) – Hiển thị danh sách
        public IActionResult Index()
        {
            // Dùng Include để load thông tin Major liên quan (Eager Loading)
            var learners = db.Learners.Include(m => m.Major).ToList(); // [cite: 451]
            return View(learners);
        }

        // 8.2 Create (C) – Thêm mới
        
        [HttpGet] // [cite: 545]
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
