using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Pages.ChiSoDienNuoc;

[Authorize(Policy = "QuanLy")]
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    public CreateModel(AppDbContext db) => _db = db;
    [BindProperty] public global::QuanLyNhaTro.Models.ChiSoDienNuoc ChiSo { get; set; } = new();
    public SelectList PhongSelect { get; set; } = null!;

    public async Task OnGetAsync(CancellationToken ct)
    {
        ChiSo.Thang = DateTime.Today.Month;
        ChiSo.Nam = DateTime.Today.Year;
        ChiSo.DonGiaDien = 3500;
        ChiSo.DonGiaNuoc = 15000;
        await LoadAsync(ct);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        await LoadAsync(ct);
        if (ChiSo.ChiSoDienMoi < ChiSo.ChiSoDienCu || ChiSo.ChiSoNuocMoi < ChiSo.ChiSoNuocCu)
        {
            ModelState.AddModelError(string.Empty, "Chỉ số mới phải >= chỉ số cũ.");
            return Page();
        }
        if (await _db.ChiSoDienNuocs.AnyAsync(c => c.PhongId == ChiSo.PhongId && c.Thang == ChiSo.Thang && c.Nam == ChiSo.Nam, ct))
        {
            ModelState.AddModelError(string.Empty, "Đã có chỉ số cho phòng và kỳ này.");
            return Page();
        }
        if (!ModelState.IsValid) return Page();
        _db.ChiSoDienNuocs.Add(ChiSo);
        await _db.SaveChangesAsync(ct);
        return RedirectToPage("Index");
    }

    private async Task LoadAsync(CancellationToken ct)
    {
        var phongs = await _db.Phongs.OrderBy(p => p.TenPhong).ToListAsync(ct);
        PhongSelect = new SelectList(phongs, nameof(global::QuanLyNhaTro.Models.Phong.PhongId), nameof(global::QuanLyNhaTro.Models.Phong.TenPhong), ChiSo.PhongId);
    }
}
