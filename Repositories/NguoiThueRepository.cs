using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Repositories;

public class NguoiThueRepository : INguoiThueRepository
{
    private readonly AppDbContext _db;

    public NguoiThueRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<NguoiThue>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _db.NguoiThues.AsNoTracking().OrderBy(n => n.HoTen).ToListAsync(cancellationToken);
    }

    public async Task<NguoiThue?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _db.NguoiThues.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
    }

    public async Task AddAsync(NguoiThue nguoiThue, CancellationToken cancellationToken = default)
    {
        _db.NguoiThues.Add(nguoiThue);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(NguoiThue nguoiThue, CancellationToken cancellationToken = default)
    {
        _db.NguoiThues.Update(nguoiThue);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _db.NguoiThues.FindAsync(new object[] { id }, cancellationToken);
        if (entity is null) return;
        _db.NguoiThues.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return _db.NguoiThues.AnyAsync(n => n.Id == id, cancellationToken);
    }
}
