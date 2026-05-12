using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Repositories;

public class PhongRepository : IPhongRepository
{
    private readonly AppDbContext _db;

    public PhongRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<Phong>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Phongs.AsNoTracking().OrderBy(p => p.TenPhong).ToListAsync(cancellationToken);
    }

    public async Task<Phong?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _db.Phongs.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task AddAsync(Phong phong, CancellationToken cancellationToken = default)
    {
        _db.Phongs.Add(phong);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Phong phong, CancellationToken cancellationToken = default)
    {
        _db.Phongs.Update(phong);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _db.Phongs.FindAsync(new object[] { id }, cancellationToken);
        if (entity is null) return;
        _db.Phongs.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return _db.Phongs.AnyAsync(p => p.Id == id, cancellationToken);
    }
}
