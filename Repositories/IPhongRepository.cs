using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Repositories;

public interface IPhongRepository
{
    Task<IReadOnlyList<Phong>> GetAllAsync(string? search = null, string? trangThai = null, CancellationToken ct = default);
    Task<Phong?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(Phong phong, CancellationToken ct = default);
    Task UpdateAsync(Phong phong, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
