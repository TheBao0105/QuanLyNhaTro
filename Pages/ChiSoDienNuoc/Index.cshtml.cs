using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Pages.ChiSoDienNuoc;

[Authorize(Policy = "QuanLy")]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) => _db = db;
    public IList<global::QuanLyNhaTro.Models.ChiSoDienNuoc> Items { get; set; } = new List<global::QuanLyNhaTro.Models.ChiSoDienNuoc>();
    public async Task OnGetAsync(CancellationToken ct) =>
        Items = await _db.ChiSoDienNuocs.AsNoTracking().Include(c => c.Phong)
            .OrderByDescending(c => c.Nam).ThenByDescending(c => c.Thang).ToListAsync(ct);
}
