using Microsoft.EntityFrameworkCore;
using VuVanDoan_CNTT1_231230746.Models;

var builder = WebApplication.CreateBuilder(args);

// --- BẮT ĐẦU ĐOẠN CẦN THÊM ---
builder.Services.AddDbContext<VanTai2512V1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// --- KẾT THÚC ĐOẠN CẦN THÊM ---

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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
