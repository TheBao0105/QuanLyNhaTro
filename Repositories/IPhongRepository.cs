using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Repositories;

public interface IPhongRepository
{
    Task<IReadOnlyList<Phong>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Phong?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(Phong phong, CancellationToken cancellationToken = default);
    Task UpdateAsync(Phong phong, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
