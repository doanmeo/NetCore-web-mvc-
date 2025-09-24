using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Employee
    {
        [Key] // Đánh dấu Id là khóa chính
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên đầy đủ là bắt buộc")] // Thêm validation
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
        public string FullName { get; set; }

        [StringLength(10, ErrorMessage = "Giới tính không được vượt quá 10 ký tự")]
        public string Gender { get; set; } // Ví dụ: "Nam", "Nữ", "Khác"

        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")] // Validation cho số điện thoại
        [StringLength(15)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")] // Validation cho email
        [StringLength(100)]
        public string Email { get; set; }

        [Range(0, 999999999.99, ErrorMessage = "Mức lương không hợp lệ")]
        public decimal Salary { get; set; }

        [StringLength(50)]
        public string Status { get; set; } // Ví dụ: "Active", "Inactive", "On Leave"
    }
}
