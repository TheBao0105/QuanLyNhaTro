using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Pages.ThongBao;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;
    public DetailsModel(AppDbContext db) => _db = db;
    public global::QuanLyNhaTro.Models.ThongBao? ThongBao { get; private set; }
    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        ThongBao = await _db.ThongBaos.FindAsync(new object[] { id }, ct);
        if (ThongBao is null) return NotFound();
        if (!ThongBao.DaDoc) { ThongBao.DaDoc = true; await _db.SaveChangesAsync(ct); }
        return Page();
    }
}
