using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Repositories;

public interface INguoiThueRepository
{
    Task<IReadOnlyList<NguoiThue>> GetAllAsync(string? search = null, CancellationToken ct = default);
    Task<NguoiThue?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> CccdTonTaiAsync(string cccd, int? excludeId = null, CancellationToken ct = default);
    Task AddAsync(NguoiThue entity, CancellationToken ct = default);
    Task UpdateAsync(NguoiThue entity, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
