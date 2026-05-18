using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Services;

public class AuthService
{
    private readonly AppDbContext _db;

    public AuthService(AppDbContext db) => _db = db;

    public async Task<TaiKhoan?> XacThucAsync(string tenDangNhap, string matKhau, CancellationToken ct = default)
    {
        var user = await _db.TaiKhoans.FirstOrDefaultAsync(
            t => t.TenDangNhap == tenDangNhap && t.TrangThai, ct);
        if (user is null || !BCrypt.Net.BCrypt.Verify(matKhau, user.MatKhauHash))
            return null;
        return user;
    }

    public static bool CoQuyenQuanLy(string vaiTro) =>
        vaiTro is TrangThaiConstants.VaiTro.Admin
            or TrangThaiConstants.VaiTro.ChuTro
            or TrangThaiConstants.VaiTro.NhanVien;
}
