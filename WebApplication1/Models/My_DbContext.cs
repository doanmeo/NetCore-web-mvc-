using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class My_DbContext : DbContext
    {
        public My_DbContext(DbContextOptions<My_DbContext> options) : base(options)
        {
        }

        // Định nghĩa DbSet cho mỗi Model bạn muốn ánh xạ thành bảng trong database
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Thêm dữ liệu mẫu cho Employee
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FullName = "Nguyễn Văn A", Gender = "Nam", Phone = "0987123456", Email = "a.nguyen@example.com", Salary = 15000000, Status = "Active" },
                new Employee { Id = 2, FullName = "Trần Thị B", Gender = "Nữ", Phone = "0912345678", Email = "b.tran@example.com", Salary = 18000000, Status = "Active" },
                new Employee { Id = 3, FullName = "Lê Văn C", Gender = "Nam", Phone = "0901234567", Email = "c.le@example.com", Salary = 12000000, Status = "On Leave" }
            );

     
        }
    }
}
