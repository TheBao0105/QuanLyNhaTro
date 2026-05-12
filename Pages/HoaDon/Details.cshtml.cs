using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using Entity = QuanLyNhaTro.Models.HoaDon;

namespace QuanLyNhaTro.Pages.HoaDon;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;

    public DetailsModel(AppDbContext db) => _db = db;

    public Entity? HoaDon { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        HoaDon = await _db.HoaDons
            .AsNoTracking()
            .Include(h => h.HopDong)
            .ThenInclude(d => d!.Phong)
            .Include(h => h.HopDong)
            .ThenInclude(d => d!.NguoiThue)
            .Include(h => h.ThanhToans)
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (HoaDon is null)
            return NotFound();

        return Page();
    }
}
