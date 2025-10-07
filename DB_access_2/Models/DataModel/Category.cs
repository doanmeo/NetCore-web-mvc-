using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB_access_2.Models.DataModel
{
    [Table("Categories")]
    public class Category
    {
        [DisplayName("Mã loại")]
        public int CategoryId { get; set; }

        [DisplayName("Tên loại")]
        [StringLength(100)]
        public string CategoryName { get; set; }

        // Thuộc tính quan hệ
        //1-n với Book
        public ICollection<Book> Books { get; set; }
    }
}
