using Microsoft.EntityFrameworkCore;
using WebNhacCu.Data;
using WebNhacCu.Interfaces;
using WebNhacCu.Models.EF;
using WebNhacCu.Services;
using WebNhacCu.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WebHeThongBanNhacCuContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("WebHeThongBanNhacCudb")));
builder.Services.AddScoped<IProductService, ProductService>(); 
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<WebHeThongBanNhacCuContext>();

    DataSeeder.Seed(context);
}

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
// gọi lệnh thực hiện Admin trong program
app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
    );

app.Run();
