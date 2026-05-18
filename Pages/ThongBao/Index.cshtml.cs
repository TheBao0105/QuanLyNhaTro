using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Pages.ThongBao;

[Authorize]
public class IndexModel : PageModel
{
    private readonly AppDbContext _db;
    public IndexModel(AppDbContext db) => _db = db;
    public IList<global::QuanLyNhaTro.Models.ThongBao> Items { get; set; } = new List<global::QuanLyNhaTro.Models.ThongBao>();
    public async Task OnGetAsync(CancellationToken ct) =>
        Items = await _db.ThongBaos.AsNoTracking().OrderByDescending(t => t.NgayTao).ToListAsync(ct);
}
