using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ktra02.Models
{
    // Bước 1: Khai báo class HangHoa là partial và gán Metadata cho nó
    //meta data dùng để liên kết class HangHoa với class HangHoaMetaData
    [ModelMetadataType(typeof(HangHoaMetaData))]
    public partial class HangHoa
    {
        // Để trống, không cần viết gì thêm
    }

    // Bước 2: Tạo class Metadata chứa các luật Validation
    public class HangHoaMetaData
    {
        [Required(ErrorMessage = "Tên hàng hóa là bắt buộc")]
        public string TenHang { get; set; }

        [Required(ErrorMessage = "Đơn giá là bắt buộc")]
        [Range(100, 5000, ErrorMessage = "Giá phải nằm trong khoảng từ 100 đến 5000")]
        public double? Gia { get; set; }

        [Required(ErrorMessage = "Ảnh sản phẩm là bắt buộc")]
        // Regex kiểm tra đuôi file ảnh (không phân biệt hoa thường)
        [RegularExpression(@"^.*\.(jpg|png|gif|tiff)$", ErrorMessage = "Tên file ảnh phải có đuôi: .jpg, .png, .gif, .tiff")]
        public string Anh { get; set; }
    }
}
