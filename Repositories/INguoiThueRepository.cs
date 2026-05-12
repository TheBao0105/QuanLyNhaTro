using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Repositories;

public interface INguoiThueRepository
{
    Task<IReadOnlyList<NguoiThue>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<NguoiThue?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task AddAsync(NguoiThue nguoiThue, CancellationToken cancellationToken = default);
    Task UpdateAsync(NguoiThue nguoiThue, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
}
