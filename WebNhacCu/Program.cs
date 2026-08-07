using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using WebNhacCu.Data;
using WebNhacCu.Interfaces;
using WebNhacCu.Models.EF;
using WebNhacCu.Services;
using WebNhacCu.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm các dịch vụ vào DI Container
builder.Services.AddControllersWithViews();

// Cấu hình DbContext (Bỏ qua cảnh báo Pending Model Changes Warning nếu có)
builder.Services.AddDbContext<WebHeThongBanNhacCuContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("WebHeThongBanNhacCudb"))
    .ConfigureWarnings(w =>
        w.Ignore(RelationalEventId.PendingModelChangesWarning))
);

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBrandService, BrandService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WebHeThongBanNhacCuContext>();
    DataSeeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Cấu hình Route cho Areas (Admin) và Mặc định
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Nếu Hào KHÔNG DÙNG DataSeeder thì xóa hoặc comment dòng 15 bị lỗi lại:
// DataSeeder.SeedData(context); 

// HOẶC nếu Hào DÙNG DataSeeder thì phải gọi qua Scope như sau:
/*
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<WebNhacCuContext>();
    // DataSeeder.SeedData(context); // Bật lại nếu dự án có file DataSeeder.cs
}
*/

app.Run();