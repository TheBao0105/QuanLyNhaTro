using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using ThanhToanModel = QuanLyNhaTro.Models.ThanhToan;
using HoaDonModel = QuanLyNhaTro.Models.HoaDon;
using QuanLyNhaTro.Services;

namespace QuanLyNhaTro.Pages.ThanhToan;

[Authorize(Policy = "QuanLy")]
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly HoaDonService _svc;

    public CreateModel(AppDbContext db, HoaDonService svc) { _db = db; _svc = svc; }

    [BindProperty]
    public ThanhToanModel ThanhToan { get; set; } = new();

    public HoaDonModel? HoaDonInfo { get; private set; }
    public SelectList HoaDonSelect { get; private set; } = null!;

    public async Task OnGetAsync(int? hoaDonId, CancellationToken ct)
    {
        if (hoaDonId.HasValue) ThanhToan.HoaDonId = hoaDonId.Value;
        await LoadAsync(ct);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        await LoadAsync(ct);
        if (!ModelState.IsValid) return Page();

        var (ok, err) = await _svc.GhiNhanThanhToanAsync(ThanhToan, User.Identity?.Name, ct);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, err ?? "Không ghi nhận được.");
            return Page();
        }
        TempData["Success"] = "Đã ghi nhận thanh toán.";
        return RedirectToPage("Index");
    }

    private async Task LoadAsync(CancellationToken ct)
    {
        var list = await _db.HoaDons.AsNoTracking()
            .Include(h => h.Phong)
            .Where(h => h.ConNo > 0)
            .OrderByDescending(h => h.Nam).ThenByDescending(h => h.Thang)
            .ToListAsync(ct);
        HoaDonSelect = new SelectList(
            list.Select(h => new { h.HoaDonId, Text = $"{h.Phong.TenPhong} — {h.Thang}/{h.Nam} (nợ {h.ConNo:N0})" }),
            "HoaDonId", "Text", ThanhToan.HoaDonId);
        if (ThanhToan.HoaDonId > 0)
            HoaDonInfo = list.FirstOrDefault(h => h.HoaDonId == ThanhToan.HoaDonId);
    }
}
