using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.ViewModels;

namespace QuanLyNhaTro.Services;

public class ThongKeService
{
    private readonly AppDbContext _db;

    public ThongKeService(AppDbContext db) => _db = db;

    public async Task<DashboardVM> LayThongKeAsync(CancellationToken cancellationToken = default)
    {
        var vm = new DashboardVM
        {
            TongPhong = await _db.Phongs.CountAsync(cancellationToken),
            PhongTrong = await _db.Phongs.CountAsync(p => p.TrangThai == "Trong", cancellationToken),
            TongNguoiThue = await _db.NguoiThues.CountAsync(cancellationToken),
            HopDongHieuLuc = await _db.HopDongs.CountAsync(h => h.TrangThai == "HieuLuc", cancellationToken),
            HoaDonChuaThu = await _db.HoaDons.CountAsync(h => h.TrangThai == "ChuaThanhToan" || h.TrangThai == "MotPhan", cancellationToken)
        };

        vm.TongNoThang = await _db.HoaDons
            .Where(h => h.TrangThai != "DaThanhToan")
            .SumAsync(h => h.TongTien, cancellationToken);

        return vm;
    }
}
