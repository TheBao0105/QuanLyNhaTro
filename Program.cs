using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Repositories;
using QuanLyNhaTro.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPhongRepository, PhongRepository>();
builder.Services.AddScoped<INguoiThueRepository, NguoiThueRepository>();
builder.Services.AddScoped<IHoaDonRepository, HoaDonRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<HopDongService>();
builder.Services.AddScoped<HoaDonService>();
builder.Services.AddScoped<ThongKeService>();
builder.Services.AddScoped<IUploadService, UploadService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/TaiKhoan/Login";
        o.LogoutPath = "/TaiKhoan/Logout";
        o.AccessDeniedPath = "/TaiKhoan/Login";
        o.ExpireTimeSpan = TimeSpan.FromDays(7);
        o.SlidingExpiration = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("QuanLy", p => p.RequireRole(
        TrangThaiConstants.VaiTro.Admin,
        TrangThaiConstants.VaiTro.ChuTro,
        TrangThaiConstants.VaiTro.NhanVien));
    options.AddPolicy("AdminOnly", p => p.RequireRole(TrangThaiConstants.VaiTro.Admin));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/");
    options.Conventions.AllowAnonymousToPage("/Index");
    options.Conventions.AllowAnonymousToPage("/Privacy");
    options.Conventions.AllowAnonymousToPage("/Error");
    options.Conventions.AllowAnonymousToFolder("/TaiKhoan");
});

builder.Services.AddControllers();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

Directory.CreateDirectory(Path.Combine(app.Environment.WebRootPath, "uploads"));

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var log = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DbInit");
    await DbInitializer.InitializeAsync(db, log);
}

app.MapControllers();
app.MapRazorPages();

app.Run();
