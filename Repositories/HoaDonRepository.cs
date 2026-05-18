using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Repositories;

public class HoaDonRepository : IHoaDonRepository
{
    private readonly AppDbContext _db;
    public HoaDonRepository(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<HoaDon>> GetAllAsync(int? thang = null, int? nam = null, string? trangThai = null, CancellationToken ct = default)
    {
        var q = _db.HoaDons.AsNoTracking().Include(h => h.Phong).AsQueryable();
        if (thang.HasValue) q = q.Where(h => h.Thang == thang);
        if (nam.HasValue) q = q.Where(h => h.Nam == nam);
        if (!string.IsNullOrWhiteSpace(trangThai)) q = q.Where(h => h.TrangThai == trangThai);
        return await q.OrderByDescending(h => h.Nam).ThenByDescending(h => h.Thang).ToListAsync(ct);
    }

    public Task<HoaDon?> GetByIdAsync(int id, CancellationToken ct = default) =>
        _db.HoaDons
            .Include(h => h.Phong)
            .Include(h => h.ChiTietHoaDons)
            .Include(h => h.ThanhToans)
            .FirstOrDefaultAsync(h => h.HoaDonId == id, ct);
}
