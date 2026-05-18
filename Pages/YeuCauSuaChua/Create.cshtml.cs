using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Pages.YeuCauSuaChua;

[Authorize]
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    public CreateModel(AppDbContext db) => _db = db;
    [BindProperty] public global::QuanLyNhaTro.Models.YeuCauSuaChua YeuCau { get; set; } = new();
    public SelectList PhongSelect { get; set; } = null!;
    public async Task OnGetAsync(CancellationToken ct) { YeuCau.NguoiGui = User.Identity?.Name; await Load(ct); }
    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        await Load(ct);
        if (!ModelState.IsValid) return Page();
        _db.YeuCauSuaChuas.Add(YeuCau);
        await _db.SaveChangesAsync(ct);
        return RedirectToPage("Index");
    }
    private async Task Load(CancellationToken ct)
    {
        var p = await _db.Phongs.OrderBy(x => x.TenPhong).ToListAsync(ct);
        PhongSelect = new SelectList(p, nameof(global::QuanLyNhaTro.Models.Phong.PhongId), nameof(global::QuanLyNhaTro.Models.Phong.TenPhong));
    }
}
