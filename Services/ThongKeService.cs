using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.ViewModels;

namespace QuanLyNhaTro.Services;

public class ThongKeService
{
    private readonly AppDbContext _db;

    public ThongKeService(AppDbContext db) => _db = db;

    public async Task<DashboardVM> LayDashboardAsync(CancellationToken ct = default)
    {
        var now = DateTime.Today;
        var thang = now.Month;
        var nam = now.Year;

        var vm = new DashboardVM
        {
            TongPhong = await _db.Phongs.CountAsync(ct),
            PhongTrong = await _db.Phongs.CountAsync(p => p.TrangThai == TrangThaiConstants.Phong.ConTrong, ct),
            PhongDangThue = await _db.Phongs.CountAsync(p => p.TrangThai == TrangThaiConstants.Phong.DangThue, ct),
            PhongBaoTri = await _db.Phongs.CountAsync(p => p.TrangThai == TrangThaiConstants.Phong.BaoTri, ct),
            TongNguoiThue = await _db.NguoiThues.CountAsync(ct),
            HopDongHieuLuc = await _db.HopDongs.CountAsync(h => h.TrangThai == TrangThaiConstants.HopDong.HieuLuc, ct),
            HoaDonChuaThu = await _db.HoaDons.CountAsync(
                h => h.TrangThai != TrangThaiConstants.HoaDon.DaThanhToan, ct),
            TongNo = await _db.HoaDons.SumAsync(h => h.ConNo, ct),
            DoanhThuThang = await _db.ThanhToans
                .Where(t => t.NgayThanhToan.Month == thang && t.NgayThanhToan.Year == nam)
                .SumAsync(t => t.SoTien, ct),
            DoanhThuNam = await _db.ThanhToans
                .Where(t => t.NgayThanhToan.Year == nam)
                .SumAsync(t => t.SoTien, ct)
        };

        var sapHetHan = now.AddDays(30);
        vm.HopDongSapHetHan = await _db.HopDongs
            .Include(h => h.Phong)
            .Include(h => h.NguoiThue)
            .Where(h => h.TrangThai == TrangThaiConstants.HopDong.HieuLuc
                && h.NgayKetThuc != null && h.NgayKetThuc <= sapHetHan)
            .OrderBy(h => h.NgayKetThuc)
            .Take(10)
            .Select(h => new HopDongSapHetHanItem
            {
                HopDongId = h.HopDongId,
                TenPhong = h.Phong.TenPhong,
                TenNguoiThue = h.NguoiThue.HoTen,
                NgayKetThuc = h.NgayKetThuc
            })
            .ToListAsync(ct);

        vm.HoaDonQuaHan = await _db.HoaDons
            .Include(h => h.Phong)
            .Where(h => h.TrangThai == TrangThaiConstants.HoaDon.QuaHan
                || (h.HanThanhToan < now && h.ConNo > 0))
            .OrderBy(h => h.HanThanhToan)
            .Take(10)
            .Select(h => new HoaDonQuaHanItem
            {
                HoaDonId = h.HoaDonId,
                TenPhong = h.Phong.TenPhong,
                Thang = h.Thang,
                Nam = h.Nam,
                ConNo = h.ConNo
            })
            .ToListAsync(ct);

        return vm;
    }
}
