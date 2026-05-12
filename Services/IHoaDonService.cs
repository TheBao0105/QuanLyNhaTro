using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Services;

public interface IHoaDonService
{
    void TinhLaiTongTien(HoaDon hoaDon);
    Task SyncTrangThaiTheoThanhToanAsync(HoaDon hoaDon, CancellationToken cancellationToken = default);
}
