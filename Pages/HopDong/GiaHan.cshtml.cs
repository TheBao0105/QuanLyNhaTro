using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;

namespace QuanLyNhaTro.Pages.HopDong;

[Authorize(Policy = "QuanLy")]
public class GiaHanModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly HopDongService _svc;

    public GiaHanModel(AppDbContext db, HopDongService svc) { _db = db; _svc = svc; }

    public global::QuanLyNhaTro.Models.HopDong? HopDong { get; private set; }

    [BindProperty]
    public DateTime NgayKetThucMoi { get; set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        HopDong = await _db.HopDongs.Include(h => h.Phong).FirstOrDefaultAsync(h => h.HopDongId == id, ct);
        if (HopDong is null) return NotFound();
        NgayKetThucMoi = HopDong.NgayKetThuc?.AddMonths(6) ?? DateTime.Today.AddMonths(6);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken ct)
    {
        var (ok, err) = await _svc.GiaHanHopDongAsync(id, NgayKetThucMoi, ct);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, err ?? "Lỗi.");
            return await OnGetAsync(id, ct);
        }
        TempData["Success"] = "Đã gia hạn hợp đồng.";
        return RedirectToPage("Index");
    }
}
