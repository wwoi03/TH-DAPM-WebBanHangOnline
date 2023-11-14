using Microsoft.EntityFrameworkCore;
using TH_DAPM_WebBanHangOnline.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Kết nối database
builder.Services.AddDbContext<DatabaseContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")));

// Kích hoạt Session
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Sử dụng session
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomeCustomer}/{action=HomePage}/{id?}");
/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Dashboard}/{id?}");*/
//link thêm vào giở hàng trang chi tiết
//app.MapControllerRoute(
//	name: "AddToCart",
//	pattern: "{controller=CartCustomer}/{action=AddToCartToProductDetals}/{productid}/{quantity}",
//	defaults: new { controller = "CartCustomer", action = "AddToCartToProductDetals" });
//app.MapControllerRoute(
//	name: "EditQuantityPro",
//    pattern: "{controller=CartCustomer}/{action=EditQuantityPro}/{quantity}/{cartId}",
//	defaults: new { controller = "CartCustomer", action = "EditQuantityPro" }
//	);
app.Run();
