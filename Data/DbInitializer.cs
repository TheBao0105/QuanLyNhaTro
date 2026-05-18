using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(AppDbContext db, ILogger logger, CancellationToken ct = default)
    {
        var pending = await db.Database.GetPendingMigrationsAsync(ct);
        if (pending.Any())
            logger.LogInformation("Áp dụng migration: {Names}", string.Join(", ", pending));

        await db.Database.MigrateAsync(ct);

        if (await db.TaiKhoans.AnyAsync(ct)) return;

        db.TaiKhoans.Add(new TaiKhoan
        {
            TenDangNhap = "admin",
            MatKhauHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            HoTen = "Quản trị hệ thống",
            VaiTro = TrangThaiConstants.VaiTro.Admin,
            TrangThai = true,
            NgayTao = DateTime.UtcNow
        });

        db.DichVus.AddRange(
            new DichVu { TenDichVu = "Internet", DonGia = 100_000, DonViTinh = "tháng", TrangThai = true },
            new DichVu { TenDichVu = "Rác", DonGia = 30_000, DonViTinh = "tháng", TrangThai = true },
            new DichVu { TenDichVu = "Gửi xe", DonGia = 150_000, DonViTinh = "tháng", TrangThai = true }
        );

        db.Phongs.AddRange(
            new Phong { TenPhong = "P101", GiaThue = 3_500_000, DienTich = 20, SoNguoiToiDa = 2, TrangThai = TrangThaiConstants.Phong.ConTrong },
            new Phong { TenPhong = "P102", GiaThue = 4_000_000, DienTich = 25, SoNguoiToiDa = 3, TrangThai = TrangThaiConstants.Phong.ConTrong }
        );

        await db.SaveChangesAsync(ct);
        logger.LogInformation("Đã seed dữ liệu mẫu. Đăng nhập: admin / Admin@123");
    }
}
