using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        private readonly My_DbContext _context;

        public AdminController(My_DbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            ViewData["Title"] = "Quản lý Nhân viên";
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }
        // --- EMPLOYEE - DETAILS ---
        public async Task<IActionResult> EmployeeDetails(int? id) // int? để cho phép id là null
        {
            if (id == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không có id
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id); // Tìm nhân viên theo id

            if (employee == null)
            {
                return NotFound(); // Trả về lỗi 404 nếu không tìm thấy nhân viên
            }

            ViewData["Title"] = "Chi tiết Nhân viên";
            return View(employee);
        }

        // --- EMPLOYEE - CREATE (GET) ---
        public IActionResult CreateEmployee()
        {
            ViewData["Title"] = "Thêm Nhân viên mới";
            return View(); // Trả về View với form trống
        }

        // --- EMPLOYEE - CREATE (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken] // Chống tấn công CSRF
        public async Task<IActionResult> CreateEmployee([Bind("FullName,Gender,Phone,Email,Salary,Status")] Employee employee)
        {
            if (ModelState.IsValid) // Kiểm tra Validation Rules trong Model
            {
                _context.Add(employee); // Thêm nhân viên vào DbContext
                await _context.SaveChangesAsync(); // Lưu thay đổi vào Database
                TempData["SuccessMessage"] = "Nhân viên đã được thêm thành công."; // Thông báo thành công
                return RedirectToAction(nameof( EmployeeList)); // Chuyển hướng về trang danh sách
            }
            ViewData["Title"] = "Thêm Nhân viên mới";
            return View(employee); // Nếu có lỗi validation, hiển thị lại form với dữ liệu đã nhập
        }

  

        // --- EMPLOYEE - DELETE (GET - Hiển thị xác nhận) ---
        public async Task<IActionResult> DeleteEmployee(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            ViewData["Title"] = "Xóa Nhân viên";
            return View(employee); // Trả về View xác nhận xóa
        }

        // --- EMPLOYEE - DELETE (POST - Thực hiện xóa) ---
        [HttpPost, ActionName("DeleteEmployee")] // Chỉ định ActionName để tránh trùng lặp
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEmployeeConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee); // Xóa nhân viên khỏi DbContext
                await _context.SaveChangesAsync(); // Lưu thay đổi vào Database
                TempData["SuccessMessage"] = "Nhân viên đã được xóa thành công.";
            }
            return RedirectToAction(nameof(EmployeeList));
        }

        // Hàm kiểm tra sự tồn tại của nhân viên
        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
        // Add this action to fix CS0103: The name 'EmployeeList' does not exist in the current context
        public async Task<IActionResult> EmployeeList()
        {
            ViewData["Title"] = "Danh sách Nhân viên";
            var employees = await _context.Employees.ToListAsync();
            return View(employees);
        }
    }
}
