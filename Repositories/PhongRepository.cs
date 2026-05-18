using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Repositories;

public class PhongRepository : IPhongRepository
{
    private readonly AppDbContext _db;
    public PhongRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<Phong>> GetAllAsync(string? search = null, string? trangThai = null, CancellationToken ct = default)
    {
        var q = _db.Phongs.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
            q = q.Where(p => p.TenPhong.Contains(search));
        if (!string.IsNullOrWhiteSpace(trangThai))
            q = q.Where(p => p.TrangThai == trangThai);
        return await q.OrderBy(p => p.TenPhong).ToListAsync(ct);
    }

    public Task<Phong?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _db.Phongs.FirstOrDefaultAsync(p => p.PhongId == id, ct);

    public async Task AddAsync(Phong phong, CancellationToken ct = default)
    {
        phong.NgayTao = DateTime.UtcNow;
        _db.Phongs.Add(phong);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(Phong phong, CancellationToken ct = default)
    {
        _db.Phongs.Update(phong);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var e = await _db.Phongs.FindAsync(new object[] { id }, ct);
        if (e is null) return;
        _db.Phongs.Remove(e);
        await _db.SaveChangesAsync(ct);
    }
}
