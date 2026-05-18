using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Repositories;

public interface IHoaDonRepository
{
    Task<IReadOnlyList<HoaDon>> GetAllAsync(int? thang = null, int? nam = null, string? trangThai = null, CancellationToken ct = default);
    Task<HoaDon?> GetByIdAsync(int id, CancellationToken ct = default);
}
