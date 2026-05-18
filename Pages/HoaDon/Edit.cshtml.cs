using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Services;

namespace QuanLyNhaTro.Pages.HoaDon;

[Authorize(Policy = "QuanLy")]
public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly HoaDonService _svc;

    public EditModel(AppDbContext db, HoaDonService svc) { _db = db; _svc = svc; }

    [BindProperty]
    public HoaDonEntity Invoice { get; set; } = new();

    public SelectList PhongSelect { get; private set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var entity = await _db.HoaDons.FirstOrDefaultAsync(h => h.HoaDonId == id, ct);
        if (entity is null) return NotFound();
        Invoice = entity;
        await LoadPhongSelectAsync(ct);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        await LoadPhongSelectAsync(ct);
        if (!ModelState.IsValid) return Page();
        _svc.TinhLaiTong(Invoice);
        _db.HoaDons.Update(Invoice);
        await _svc.CapNhatTrangThaiAsync(Invoice, ct);
        TempData["Success"] = "Đã cập nhật hóa đơn.";
        return RedirectToPage("Index");
    }

    private async Task LoadPhongSelectAsync(CancellationToken ct)
    {
        var phongs = await _db.Phongs.AsNoTracking().OrderBy(p => p.TenPhong).ToListAsync(ct);
        PhongSelect = new SelectList(phongs, nameof(PhongEntity.PhongId), nameof(PhongEntity.TenPhong), Invoice.PhongId);
    }
}
