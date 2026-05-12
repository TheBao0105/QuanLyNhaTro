using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Services;

public class HoaDonService : IHoaDonService
{
    private readonly AppDbContext _db;

    public HoaDonService(AppDbContext db) => _db = db;

    public void TinhLaiTongTien(HoaDon hoaDon)
    {
        hoaDon.TongTien = hoaDon.TienPhong + hoaDon.TienDien + hoaDon.TienNuoc + hoaDon.TienDichVu;
    }

    public async Task SyncTrangThaiTheoThanhToanAsync(HoaDon hoaDon, CancellationToken cancellationToken = default)
    {
        var daThu = await _db.ThanhToans.Where(t => t.HoaDonId == hoaDon.Id).SumAsync(t => t.SoTien, cancellationToken);
        if (daThu >= hoaDon.TongTien && hoaDon.TongTien > 0)
            hoaDon.TrangThai = "DaThanhToan";
        else if (daThu > 0)
            hoaDon.TrangThai = "MotPhan";
        else
            hoaDon.TrangThai = "ChuaThanhToan";
    }
}
