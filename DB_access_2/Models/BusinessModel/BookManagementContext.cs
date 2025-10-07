using Microsoft.EntityFrameworkCore;

namespace DB_access_2.Models.BusinessModel
{
    public class BookManagementContext: DbContext
    {
        public BookManagementContext(DbContextOptions<BookManagementContext> options) : base(options)
        {
        }
        //Khai báo các DbSet<T> tương ứng với các bảng trong CSDL
        public DbSet<DataModel.Book> Books { get; set; }
        public DbSet<DataModel.Category> Categories { get; set; }
        public DbSet<DataModel.Publisher> Publishers { get; set; }
        //Cấu hình các quan hệ giữa các bảng
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //đây gọi là Fluent API là cách cấu hình quan hệ thay thế cho Data Annotations
            modelBuilder.Entity<DataModel.Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<DataModel.Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
