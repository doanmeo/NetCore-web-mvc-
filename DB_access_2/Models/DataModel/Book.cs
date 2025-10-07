using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_access_2.Models.DataModel
{
    [Table("Books")]
    public class Book
    {
        [DisplayName("Mã sách")]
        [StringLength(10)]
        public string BookId { get; set; }

        [DisplayName("Tên sách")]
        [StringLength(200)]
        public string Title { get; set; }

        [DisplayName("Tác giả")]
        [StringLength(100)]
        public string Author { get; set; }

        [DisplayName("Năm xuất bản")]
        public int? Release { get; set; }

        [DisplayName("Giá")]
        public double? Price { get; set; }

        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Hình ảnh")]
        public string Picture { get; set; }

        [DisplayName("Mã nhà xuất bản")]
        public int? PublisherId { get; set; }

        [DisplayName("Mã loại")]
        public int? CategoryId { get; set; }

        // Tạo quan hệ giữa các thực thể
        //n-1 với Category
        public Category Category { get; set; }
        //n-1 với Publisher
        public Publisher Publisher { get; set; }
    }
}
