using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Services;

public class HoaDonService
{
    private readonly AppDbContext _db;

    public HoaDonService(AppDbContext db) => _db = db;

    public void TinhLaiTong(HoaDon hd)
    {
        hd.TongTien = hd.TienPhong + hd.TienDien + hd.TienNuoc + hd.TienDichVu;
        hd.ConNo = Math.Max(0, hd.TongTien - hd.DaThanhToan);
    }

    public async Task CapNhatTrangThaiAsync(HoaDon hd, CancellationToken ct = default)
    {
        TinhLaiTong(hd);
        if (hd.ConNo <= 0 && hd.TongTien > 0)
            hd.TrangThai = TrangThaiConstants.HoaDon.DaThanhToan;
        else if (hd.DaThanhToan > 0)
            hd.TrangThai = TrangThaiConstants.HoaDon.ThanhToanMotPhan;
        else if (hd.HanThanhToan.HasValue && hd.HanThanhToan < DateTime.Today)
            hd.TrangThai = TrangThaiConstants.HoaDon.QuaHan;
        else
            hd.TrangThai = TrangThaiConstants.HoaDon.ChuaThanhToan;

        await _db.SaveChangesAsync(ct);
    }

    public async Task<(bool Ok, string? Error, HoaDon? HoaDon)> TaoHoaDonTuPhongAsync(
        int phongId, int thang, int nam, CancellationToken ct = default)
    {
        var tonTai = await _db.HoaDons.AnyAsync(h => h.PhongId == phongId && h.Thang == thang && h.Nam == nam, ct);
        if (tonTai) return (false, "Đã có hóa đơn cho phòng trong tháng này.", null);

        var hdHieuLuc = await _db.HopDongs
            .Where(h => h.PhongId == phongId && h.TrangThai == TrangThaiConstants.HopDong.HieuLuc)
            .OrderByDescending(h => h.NgayBatDau)
            .FirstOrDefaultAsync(ct);

        var chiSo = await _db.ChiSoDienNuocs
            .FirstOrDefaultAsync(c => c.PhongId == phongId && c.Thang == thang && c.Nam == nam, ct);

        var dichVuActive = await _db.DichVus.Where(d => d.TrangThai).ToListAsync(ct);

        var hd = new HoaDon
        {
            PhongId = phongId,
            Thang = thang,
            Nam = nam,
            TienPhong = hdHieuLuc?.GiaThue ?? 0,
            TienDien = chiSo?.TienDien ?? 0,
            TienNuoc = chiSo?.TienNuoc ?? 0,
            NgayLap = DateTime.UtcNow,
            HanThanhToan = new DateTime(nam, thang, 1).AddMonths(1).AddDays(9)
        };

        decimal tienDv = 0;
        foreach (var dv in dichVuActive)
        {
            var ctLine = new ChiTietHoaDon
            {
                TenKhoanThu = dv.TenDichVu,
                SoLuong = 1,
                DonGia = dv.DonGia,
                ThanhTien = dv.DonGia
            };
            hd.ChiTietHoaDons.Add(ctLine);
            tienDv += ctLine.ThanhTien;
        }
        hd.TienDichVu = tienDv;
        TinhLaiTong(hd);
        hd.ConNo = hd.TongTien;

        _db.HoaDons.Add(hd);
        await _db.SaveChangesAsync(ct);
        return (true, null, hd);
    }

    public async Task<(bool Ok, string? Error)> GhiNhanThanhToanAsync(
        ThanhToan tt, string? nguoiThu, CancellationToken ct = default)
    {
        var hd = await _db.HoaDons
            .Include(h => h.ThanhToans)
            .FirstOrDefaultAsync(h => h.HoaDonId == tt.HoaDonId, ct);
        if (hd is null) return (false, "Không tìm thấy hóa đơn.");

        TinhLaiTong(hd);
        if (tt.SoTien > hd.ConNo)
            return (false, $"Số tiền vượt quá còn nợ ({hd.ConNo:N0} đ).");

        tt.NguoiThu = nguoiThu;
        _db.ThanhToans.Add(tt);
        hd.DaThanhToan += tt.SoTien;
        TinhLaiTong(hd);
        await CapNhatTrangThaiAsync(hd, ct);
        return (true, null);
    }
}
