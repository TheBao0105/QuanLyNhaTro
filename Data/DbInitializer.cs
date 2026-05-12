using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext db, ILogger logger, CancellationToken cancellationToken = default)
    {
        await db.Database.MigrateAsync(cancellationToken);

        if (await db.TaiKhoans.AnyAsync(cancellationToken))
            return;

        var hash = BCrypt.Net.BCrypt.HashPassword("Admin@123");
        db.TaiKhoans.Add(new TaiKhoan
        {
            TenDangNhap = "admin",
            MatKhauHash = hash,
            HoTen = "Quản trị",
            VaiTro = "Admin",
            HoatDong = true
        });

        db.DichVus.AddRange(
            new DichVu { TenDichVu = "Wifi", DonGia = 50000, DonViTinh = "thang" },
            new DichVu { TenDichVu = "Giữ xe", DonGia = 100000, DonViTinh = "thang" }
        );

        db.Phongs.AddRange(
            new Phong { TenPhong = "P101", GiaThue = 3_500_000, DienTichM2 = 20, TrangThai = "Trong" },
            new Phong { TenPhong = "P102", GiaThue = 4_000_000, DienTichM2 = 25, TrangThai = "Trong" }
        );

        await db.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Đã khởi tạo dữ liệu mẫu (admin / Admin@123).");
    }
}
