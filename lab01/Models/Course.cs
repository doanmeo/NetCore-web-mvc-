using System.ComponentModel.DataAnnotations.Schema;

namespace lab01.Models
{
    public class Course
    {
        public Course()
        {
            Enrollments = new HashSet<Enrollment>();
        }

        // [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        // Dùng attribute này để CourseID không tự động tăng, 
        // mà chúng ta sẽ gán giá trị cứng (như 1050, 4022... ở bước sau)
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
