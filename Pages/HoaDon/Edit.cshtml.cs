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
public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    private readonly IHoaDonService _hoaDonService;

    public EditModel(AppDbContext db, IHoaDonService hoaDonService)
    {
        _db = db;
        _hoaDonService = hoaDonService;
    }

    [BindProperty]
    public Entity HoaDon { get; set; } = new();

    public SelectList HopDongSelect { get; private set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _db.HoaDons.FirstOrDefaultAsync(h => h.Id == id, cancellationToken);
        if (entity is null)
            return NotFound();
        HoaDon = entity;
        await LoadLookupsAsync(cancellationToken);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        await LoadLookupsAsync(cancellationToken);
        if (!ModelState.IsValid)
            return Page();

        var dup = await _db.HoaDons.AnyAsync(
            h => h.HopDongId == HoaDon.HopDongId && h.Thang == HoaDon.Thang && h.Nam == HoaDon.Nam && h.Id != HoaDon.Id,
            cancellationToken);
        if (dup)
        {
            ModelState.AddModelError(string.Empty, "Đã có hóa đơn khác cho cùng hợp đồng và kỳ.");
            return Page();
        }

        _hoaDonService.TinhLaiTongTien(HoaDon);
        _db.HoaDons.Update(HoaDon);
        await _db.SaveChangesAsync(cancellationToken);
        await _hoaDonService.SyncTrangThaiTheoThanhToanAsync(HoaDon, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return RedirectToPage("Index");
    }

    private async Task LoadLookupsAsync(CancellationToken cancellationToken)
    {
        var hopDongs = await _db.HopDongs
            .AsNoTracking()
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
