using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
namespace QuanLyNhaTro.Pages.HoaDon;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;

    public IndexModel(AppDbContext db) => _db = db;

    public List<QuanLyNhaTro.Models.HoaDon> Items { get; private set; } = new();

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Items = await _db.HoaDons
            .AsNoTracking()
            .Include(h => h.HopDong)
            .ThenInclude(d => d!.Phong)
            .Include(h => h.HopDong)
            .ThenInclude(d => d!.NguoiThue)
            .OrderByDescending(h => h.Nam)
            .ThenByDescending(h => h.Thang)
            .ToListAsync(cancellationToken);
    }
}
