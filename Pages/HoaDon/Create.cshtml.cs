using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;

namespace QuanLyNhaTro.Pages.HoaDon;

[Authorize(Policy = "QuanLy")]
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly HoaDonService _hoaDonService;

    public CreateModel(AppDbContext db, HoaDonService hoaDonService)
    {
        _db = db;
        _hoaDonService = hoaDonService;
    }

    [BindProperty]
    public int PhongId { get; set; }

    [BindProperty]
    public int Thang { get; set; }

    [BindProperty]
    public int Nam { get; set; }

    public SelectList PhongSelect { get; private set; } = null!;

    public async Task OnGetAsync(CancellationToken ct)
    {
        var now = DateTime.UtcNow;
        Thang = now.Month;
        Nam = now.Year;
        await LoadPhongAsync(ct);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        await LoadPhongAsync(ct);
        if (PhongId <= 0)
        {
            ModelState.AddModelError(nameof(PhongId), "Chọn phòng.");
            return Page();
        }

        (bool ok, string? error, HoaDonEntity? _) = await _hoaDonService.TaoHoaDonTuPhongAsync(PhongId, Thang, Nam, ct);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, error ?? "Không tạo được hóa đơn.");
            return Page();
        }

        TempData["Success"] = "Đã lập hóa đơn tự động (phòng, điện nước, dịch vụ).";
        return RedirectToPage("Index");
    }

    private async Task LoadPhongAsync(CancellationToken ct)
    {
        var phongs = await _db.Phongs.AsNoTracking()
            .Where(p => p.TrangThai == TrangThaiConstants.Phong.DangThue)
            .OrderBy(p => p.TenPhong)
            .ToListAsync(ct);
        PhongSelect = new SelectList(phongs, nameof(global::QuanLyNhaTro.Models.Phong.PhongId), nameof(global::QuanLyNhaTro.Models.Phong.TenPhong), PhongId);
    }
}
