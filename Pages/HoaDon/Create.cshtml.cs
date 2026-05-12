using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Services;
using Entity = QuanLyNhaTro.Models.HoaDon;

namespace QuanLyNhaTro.Pages.HoaDon;

[Authorize]
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly IHoaDonService _hoaDonService;

    public CreateModel(AppDbContext db, IHoaDonService hoaDonService)
    {
        _db = db;
        _hoaDonService = hoaDonService;
    }

    [BindProperty]
    public Entity HoaDon { get; set; } = new();

    public SelectList HopDongSelect { get; private set; } = null!;

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        HoaDon.Thang = now.Month;
        HoaDon.Nam = now.Year;
        HoaDon.TienPhong = 0;
        HoaDon.TienDien = 0;
        HoaDon.TienNuoc = 0;
        HoaDon.TienDichVu = 0;
        HoaDon.TrangThai = "ChuaThanhToan";
        await LoadLookupsAsync(cancellationToken);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        await LoadLookupsAsync(cancellationToken);
        if (!ModelState.IsValid)
            return Page();

        var dup = await _db.HoaDons.AnyAsync(
            h => h.HopDongId == HoaDon.HopDongId && h.Thang == HoaDon.Thang && h.Nam == HoaDon.Nam,
            cancellationToken);
        if (dup)
        {
            ModelState.AddModelError(string.Empty, "Đã có hóa đơn cho hợp đồng và kỳ này.");
            return Page();
        }

        _hoaDonService.TinhLaiTongTien(HoaDon);
        HoaDon.NgayLap = DateTime.UtcNow;
        try
        {
            _db.HoaDons.Add(HoaDon);
            await _db.SaveChangesAsync(cancellationToken);
            await _hoaDonService.SyncTrangThaiTheoThanhToanAsync(HoaDon, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Không lưu được hóa đơn (trùng kỳ hoặc hợp đồng không hợp lệ).");
            return Page();
        }

        return RedirectToPage("Index");
    }

    private async Task LoadLookupsAsync(CancellationToken cancellationToken)
    {
        var hopDongs = await _db.HopDongs
            .AsNoTracking()
            .Where(h => h.TrangThai == "HieuLuc")
            .Include(h => h.Phong)
            .Include(h => h.NguoiThue)
            .OrderBy(h => h.Phong!.TenPhong)
            .ToListAsync(cancellationToken);

        HopDongSelect = new SelectList(
            hopDongs.Select(h => new { h.Id, Text = $"{h.Phong.TenPhong} — {h.NguoiThue.HoTen}" }),
            "Id",
            "Text",
            HoaDon.HopDongId);
    }
}
