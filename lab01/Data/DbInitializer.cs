using lab01.Models;
using Microsoft.EntityFrameworkCore;

namespace lab01.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SchoolContext(
                serviceProvider.GetRequiredService<DbContextOptions<SchoolContext>>()))
            {
                // 1. Tạo database nếu chưa tồn tại
                context.Database.EnsureCreated(); // 

                // 2. Kiểm tra xem đã có dữ liệu Major chưa
                 if (context.Majors.Any()) // [cite: 357]
                {
                    return; // DB đã có dữ liệu, không làm gì cả
                }

                // 3. Thêm dữ liệu Majors
                var majors = new Major[]
                {
                    new Major{MajorName="IT"}, // [cite: 362]
                    new Major{MajorName="Economics"}, // [cite: 363]
                    new Major{MajorName="Mathematics"}, // [cite: 364]
                };
                foreach (var major in majors)
                {
                    context.Majors.Add(major); // [cite: 367]
                }
                context.SaveChanges(); // [cite: 368]

                // 4. Thêm dữ liệu Learners
                var learners = new Learner[]
                {
                    new Learner { FirstMidName = "Carson", LastName = "Alexander",
                        EnrollmentDate = DateTime.Parse("2005-09-01"), MajorID = 1 }, // [cite: 370]
                    new Learner { FirstMidName = "Meredith", LastName = "Alonso",
                        EnrollmentDate = DateTime.Parse("2002-09-01"), MajorID = 2 } // [cite: 371]
                };
                foreach (Learner l in learners)
                {
                    context.Learners.Add(l); // Lỗi PDF: [cite: 375] ghi "Add(1)" -> đã sửa thành "Add(l)"
                }
                context.SaveChanges(); // [cite: 376]

                // 5. Thêm dữ liệu Courses
                var courses = new Course[]
                {
                    new Course{CourseID=1050, Title="Chemistry", Credits=3}, // [cite: 379]
                    new Course{CourseID=4022, Title="Microeconomics", Credits=3}, // [cite: 380]
                    new Course{CourseID=4041, Title="Macroeconomics", Credits=3} // [cite: 380]
                };
                foreach (Course c in courses)
                {
                    context.Courses.Add(c); // [cite: 383]
                }
                context.SaveChanges(); // [cite: 384]

                // 6. Thêm dữ liệu Enrollments
                var enrollments = new Enrollment[]
                {
                    new Enrollment {LearnerID = 1, CourseID = 1050, Grade = 5.5f}, // [cite: 387]
                    new Enrollment {LearnerID = 1, CourseID = 4022, Grade = 7.5f}, // [cite: 387]
                    new Enrollment {LearnerID = 2, CourseID = 1050, Grade = 3.5f}, // [cite: 387]
                    new Enrollment {LearnerID = 2, CourseID = 4041, Grade = 7f} // [cite: 387]
                };
                foreach (Enrollment e in enrollments)
                {
                    context.Enrollments.Add(e); // [cite: 390]
                }
                context.SaveChanges(); // [cite: 391]
            }
        }
    }
}
