using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Services;

public class HopDongService
{
    private readonly AppDbContext _db;

    public HopDongService(AppDbContext db) => _db = db;

    public async Task<(bool Ok, string? Error)> TaoHopDongAsync(HopDong hopDong, CancellationToken ct = default)
    {
        if (hopDong.NgayKetThuc.HasValue && hopDong.NgayKetThuc <= hopDong.NgayBatDau)
            return (false, "Ngày kết thúc phải sau ngày bắt đầu.");

        var phong = await _db.Phongs.FindAsync(new object[] { hopDong.PhongId }, ct);
        if (phong is null)
            return (false, "Phòng không tồn tại.");

        if (phong.TrangThai == TrangThaiConstants.Phong.DangThue)
            return (false, "Phòng đang được thuê, không tạo hợp đồng mới.");

        var coHdHieuLuc = await _db.HopDongs.AnyAsync(
            h => h.PhongId == hopDong.PhongId && h.TrangThai == TrangThaiConstants.HopDong.HieuLuc, ct);
        if (coHdHieuLuc)
            return (false, "Phòng đã có hợp đồng hiệu lực.");

        if (hopDong.GiaThue <= 0)
            hopDong.GiaThue = phong.GiaThue;

        hopDong.TrangThai = TrangThaiConstants.HopDong.HieuLuc;
        _db.HopDongs.Add(hopDong);
        phong.TrangThai = TrangThaiConstants.Phong.DangThue;
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<(bool Ok, string? Error)> KetThucHopDongAsync(int hopDongId, string trangThaiPhong, CancellationToken ct = default)
    {
        var hd = await _db.HopDongs.Include(h => h.Phong).FirstOrDefaultAsync(h => h.HopDongId == hopDongId, ct);
        if (hd is null) return (false, "Không tìm thấy hợp đồng.");

        hd.TrangThai = TrangThaiConstants.HopDong.KetThuc;
        hd.NgayKetThuc ??= DateTime.Today;
        hd.Phong.TrangThai = trangThaiPhong is TrangThaiConstants.Phong.BaoTri
            ? TrangThaiConstants.Phong.BaoTri
            : TrangThaiConstants.Phong.ConTrong;
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }

    public async Task<(bool Ok, string? Error)> GiaHanHopDongAsync(int hopDongId, DateTime ngayKetThucMoi, CancellationToken ct = default)
    {
        var hd = await _db.HopDongs.FindAsync(new object[] { hopDongId }, ct);
        if (hd is null) return (false, "Không tìm thấy hợp đồng.");
        if (ngayKetThucMoi <= hd.NgayBatDau)
            return (false, "Ngày kết thúc mới phải sau ngày bắt đầu.");

        hd.NgayKetThuc = ngayKetThucMoi;
        hd.TrangThai = TrangThaiConstants.HopDong.HieuLuc;
        await _db.SaveChangesAsync(ct);
        return (true, null);
    }
}
