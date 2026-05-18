using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Pages.HoaDon;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;

    public DetailsModel(AppDbContext db) => _db = db;

    public HoaDonEntity? Invoice { get; private set; }
    public string? NguoiThueHoTen { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        Invoice = await _db.HoaDons
            .AsNoTracking()
            .Include(h => h.Phong)
            .Include(h => h.ThanhToans)
            .FirstOrDefaultAsync(h => h.HoaDonId == id, cancellationToken);

        if (Invoice is null)
            return NotFound();

        NguoiThueHoTen = await _db.HopDongs.AsNoTracking()
            .Where(h => h.PhongId == Invoice.PhongId && h.TrangThai == TrangThaiConstants.HopDong.HieuLuc)
            .Select(h => h.NguoiThue.HoTen)
            .FirstOrDefaultAsync(cancellationToken);

        return Page();
    }
}
