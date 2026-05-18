using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Services;

namespace QuanLyNhaTro.Pages.HopDong;

[Authorize(Policy = "QuanLy")]
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly HopDongService _hopDongService;

    public CreateModel(AppDbContext db, HopDongService hopDongService)
    {
        _db = db;
        _hopDongService = hopDongService;
    }

    [BindProperty]
    public global::QuanLyNhaTro.Models.HopDong HopDong { get; set; } = new();

    public SelectList PhongSelect { get; private set; } = null!;
    public SelectList NguoiThueSelect { get; private set; } = null!;
    public int PhongCount { get; private set; }
    public int NguoiThueCount { get; private set; }

    public async Task OnGetAsync(CancellationToken ct)
    {
        HopDong.NgayBatDau = DateTime.Today;
        HopDong.TrangThai = TrangThaiConstants.HopDong.HieuLuc;
        await LoadLookupsAsync(ct);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        await LoadLookupsAsync(ct);
        if (!ModelState.IsValid) return Page();

        var (ok, error) = await _hopDongService.TaoHopDongAsync(HopDong, ct);
        if (!ok)
        {
            ModelState.AddModelError(string.Empty, error ?? "Không tạo được hợp đồng.");
            return Page();
        }
        TempData["Success"] = "Đã tạo hợp đồng.";
        return RedirectToPage("Index");
    }

    private async Task LoadLookupsAsync(CancellationToken ct)
    {
        PhongCount = await _db.Phongs.CountAsync(ct);
        NguoiThueCount = await _db.NguoiThues.CountAsync(ct);

        var phongs = await _db.Phongs.AsNoTracking()
            .Where(p => p.TrangThai != TrangThaiConstants.Phong.DangThue)
            .OrderBy(p => p.TenPhong).ToListAsync(ct);
        var nguoi = await _db.NguoiThues.AsNoTracking().OrderBy(n => n.HoTen).ToListAsync(ct);
        PhongSelect = new SelectList(phongs, nameof(global::QuanLyNhaTro.Models.Phong.PhongId), nameof(global::QuanLyNhaTro.Models.Phong.TenPhong));
        NguoiThueSelect = new SelectList(nguoi, nameof(global::QuanLyNhaTro.Models.NguoiThue.NguoiThueId), nameof(global::QuanLyNhaTro.Models.NguoiThue.HoTen));
    }
}
