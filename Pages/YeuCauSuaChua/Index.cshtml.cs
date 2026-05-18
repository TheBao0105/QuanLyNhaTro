using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Pages.YeuCauSuaChua;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) => _db = db;
    public IList<global::QuanLyNhaTro.Models.YeuCauSuaChua> Items { get; set; } = new List<global::QuanLyNhaTro.Models.YeuCauSuaChua>();
    public async Task OnGetAsync(CancellationToken ct) =>
        Items = await _db.YeuCauSuaChuas.AsNoTracking().Include(y => y.Phong)
            .OrderByDescending(y => y.NgayGui).ToListAsync(ct);
}
