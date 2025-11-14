using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab01.Models
{
    public class Student
    {
        public int Id { get; set; }//Mã sinh viên

        [Display(Name = "Họ Tên")]
        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ tên phải dài từ 4 đến 100 ký tự")] // Yêu cầu 4
        public string? Name { get; set; } //Họ tên

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@gmail\.com", ErrorMessage = "Email phải có đuôi gmail.com")] // Yêu cầu 4
        public string? Email { get; set; } //Email

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        // Yêu cầu 4: 8+ ký tự, có hoa, thường, số, ký tự đặc biệt
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
            ErrorMessage = "Mật khẩu phải từ 8 ký tự, có chữ hoa, thường, số và ký tự đặc biệt")]
        public string? Password { get; set; }//Mật khẩu

        [Display(Name = "Ngành học")]
        [Required(ErrorMessage = "Ngành học là bắt buộc")]
        public Branch? Branch { get; set; }//Ngành học

        [Display(Name = "Giới tính")]
        [Required(ErrorMessage = "Giới tính là bắt buộc")]
        public Gender? Gender { get; set; }//Giới tính

        [Display(Name = "Hệ đào tạo (Chính quy)")]
        public bool IsRegular { get; set; }//Hệ: true chính quy, false-phi chính quy

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }//Địa chỉ

        [Display(Name = "Ngày sinh")]
        [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/1963", "12/31/2005", ErrorMessage = "Năm sinh phải trong khoảng từ 1963 đến 2005")]
        public DateTime DateOfBorth { get; set; }//Ngày sinh

        // Yêu cầu 4: Thêm trường điểm
        //[Display(Name = "Điểm")]
        //[Required(ErrorMessage = "Điểm là bắt buộc")]
        //[Range(0.0, 10.0, ErrorMessage = "Điểm phải trong khoảng từ 0.0 đến 10.0")]
        //public double? Diem { get; set; }

        public string? AvatarUrl { get; set; } // Thuộc tính để lưu đường dẫn ảnh

        [NotMapped] // Báo cho hệ thống không lưu thuộc tính này vào CSDL
        public IFormFile? AvatarFile { get; set; } // Thuộc tính để nhận file từ form
    }
}
