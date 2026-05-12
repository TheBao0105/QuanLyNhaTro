using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;

namespace QuanLyNhaTro.Pages.HopDong;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db) => _db = db;

    public List<global::QuanLyNhaTro.Models.HopDong> Items { get; private set; } = new();

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Items = await _db.HopDongs
            .AsNoTracking()
            .Include(h => h.Phong)
            .Include(h => h.NguoiThue)
            .OrderByDescending(h => h.NgayBatDau)
            .ToListAsync(cancellationToken);
    }
}
