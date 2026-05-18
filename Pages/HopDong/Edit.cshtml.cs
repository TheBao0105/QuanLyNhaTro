using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;

namespace QuanLyNhaTro.Pages.HopDong;

[Authorize]
public class EditModel : PageModel
{
    private readonly AppDbContext _db;

    public EditModel(AppDbContext db) => _db = db;

    [BindProperty]
    public global::QuanLyNhaTro.Models.HopDong HopDong { get; set; } = new();

    public SelectList PhongSelect { get; private set; } = null!;
    public SelectList NguoiThueSelect { get; private set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _db.HopDongs.FirstOrDefaultAsync(h => h.HopDongId == id, cancellationToken);
        if (entity is null)
            return NotFound();
        HopDong = entity;
        await LoadLookupsAsync(cancellationToken);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        await LoadLookupsAsync(cancellationToken);
        if (!ModelState.IsValid)
            return Page();

        _db.HopDongs.Update(HopDong);
        await _db.SaveChangesAsync(cancellationToken);
        return RedirectToPage("Index");
    }

    private async Task LoadLookupsAsync(CancellationToken cancellationToken)
    {
        var phongs = await _db.Phongs.AsNoTracking().OrderBy(p => p.TenPhong).ToListAsync(cancellationToken);
        PhongSelect = new SelectList(phongs, nameof(global::QuanLyNhaTro.Models.Phong.PhongId), nameof(global::QuanLyNhaTro.Models.Phong.TenPhong), HopDong.PhongId);

        var nguoi = await _db.NguoiThues.AsNoTracking().OrderBy(n => n.HoTen).ToListAsync(cancellationToken);
        NguoiThueSelect = new SelectList(nguoi, nameof(global::QuanLyNhaTro.Models.NguoiThue.NguoiThueId), nameof(global::QuanLyNhaTro.Models.NguoiThue.HoTen), HopDong.NguoiThueId);
    }
}
