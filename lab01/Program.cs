var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
