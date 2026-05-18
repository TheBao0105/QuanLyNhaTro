using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;

namespace QuanLyNhaTro.Pages.ThanhToan;

[Authorize(Policy = "QuanLy")]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) => _db = db;

    public IList<ThanhToanEntity> Items { get; set; } = new List<ThanhToanEntity>();

    public async Task OnGetAsync(CancellationToken ct)
    {
        Items = await _db.ThanhToans.AsNoTracking()
            .Include(t => t.HoaDon).ThenInclude(h => h.Phong)
            .OrderByDescending(t => t.NgayThanhToan)
            .Take(200)
            .ToListAsync(ct);
    }
}
