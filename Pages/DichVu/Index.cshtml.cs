using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Pages.DichVu;

[Authorize(Policy = "QuanLy")]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) => _db = db;
    public IList<global::QuanLyNhaTro.Models.DichVu> Items { get; set; } = new List<global::QuanLyNhaTro.Models.DichVu>();
    public async Task OnGetAsync(CancellationToken ct) =>
        Items = await _db.DichVus.AsNoTracking().OrderBy(d => d.TenDichVu).ToListAsync(ct);
}
