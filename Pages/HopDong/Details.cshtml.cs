using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;

namespace QuanLyNhaTro.Pages.HopDong;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;

    public DetailsModel(AppDbContext db) => _db = db;

    public global::QuanLyNhaTro.Models.HopDong? HopDong { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        HopDong = await _db.HopDongs
            .AsNoTracking()
            .Include(h => h.Phong)
            .Include(h => h.NguoiThue)
            .Include(h => h.HoaDons)
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (HopDong is null)
            return NotFound();

        return Page();
    }
}
