using lab01.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// vì Configuration đã tự động đọc "Secrets.json" trong môi trường Development.
// builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("Secrets.json");

// Đăng ký SchoolContext với chuỗi kết nối "SchoolContext" từ file Secrets.json
builder.Services.AddDbContext<SchoolContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext"))); // [cite: 405-406]

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Gọi DbInitializer để tạo và seed database
 using (var scope = app.Services.CreateScope()) // [cite: 410]
{
    var services = scope.ServiceProvider; // [cite: 412]
    DbInitializer.Initialize(services); // [cite: 414]
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
// Ví dụ cấu hình route trong Program.cs
app.MapControllerRoute(
    name: "ListStudents", // Đặt tên route là "ListStudents"
    pattern: "Admin/Student/List", // Đường dẫn URL mới
    defaults: new { controller = "Student", action = "Index" } // Trỏ về đâu
);

app.MapControllerRoute(
    name: "AddStudent", // Đặt tên route là "AddStudent"
    pattern: "Admin/Student/Add", // Đường dẫn URL mới
    defaults: new { controller = "Student", action = "Create" } // Trỏ về đâu
);

app.Run();
