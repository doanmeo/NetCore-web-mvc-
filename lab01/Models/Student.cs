using System.ComponentModel.DataAnnotations.Schema;

namespace lab01.Models
{
    public class Student
    {
        public int Id { get; set; }//Mã sinh viên [cite: 36]
        public string? Name { get; set; } //Họ tên [cite: 37]
        public string? Email { get; set; } //Email [cite: 38]
        public string? Password { get; set; }//Mật khẩu [cite: 39]
        public Branch? Branch { get; set; }//Ngành học [cite: 40]
        public Gender? Gender { get; set; }//Giới tính [cite: 41]
        public bool IsRegular { get; set; }//Hệ: true chính qui, false-phi cq [cite: 42]
        public string? Address { get; set; }//Địa chỉ [cite: 43]
        public DateTime DateOfBorth { get; set; }//Ngày sinh [cite: 44]

        public string? AvatarUrl { get; set; } // Thuộc tính để lưu đường dẫn ảnh

        [NotMapped] // Báo cho hệ thống không lưu thuộc tính này vào CSDL
        public IFormFile? AvatarFile { get; set; } // Thuộc tính để nhận file từ form
    }
}
