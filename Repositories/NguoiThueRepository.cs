using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Repositories;

public class NguoiThueRepository : INguoiThueRepository
{
    private readonly AppDbContext _db;
    public NguoiThueRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<NguoiThue>> GetAllAsync(string? search = null, CancellationToken ct = default)
    {
        var q = _db.NguoiThues.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            q = q.Where(n =>
                n.HoTen.Contains(search) ||
                (n.SoDienThoai != null && n.SoDienThoai.Contains(search)) ||
                (n.CCCD != null && n.CCCD.Contains(search)));
        }
        return await q.OrderBy(n => n.HoTen).ToListAsync(ct);
    }

    public Task<NguoiThue?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _db.NguoiThues.FirstOrDefaultAsync(n => n.NguoiThueId == id, ct);

    public Task<bool> CccdTonTaiAsync(string cccd, int? excludeId = null, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(cccd)) return Task.FromResult(false);
        return _db.NguoiThues.AnyAsync(
            n => n.CCCD == cccd && (!excludeId.HasValue || n.NguoiThueId != excludeId), ct);
    }

    public async Task AddAsync(NguoiThue entity, CancellationToken ct = default)
    {
        _db.NguoiThues.Add(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task UpdateAsync(NguoiThue entity, CancellationToken ct = default)
    {
        _db.NguoiThues.Update(entity);
        await _db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var e = await _db.NguoiThues.FindAsync(new object[] { id }, ct);
        if (e is null) return;
        _db.NguoiThues.Remove(e);
        await _db.SaveChangesAsync(ct);
    }
}
