using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;

namespace QuanLyNhaTro.Pages.Phong;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;

    public DetailsModel(AppDbContext db) => _db = db;

    public global::QuanLyNhaTro.Models.Phong? Phong { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        Phong = await _db.Phongs
            .AsNoTracking()
            .Include(p => p.HopDongs)
            .ThenInclude(h => h.NguoiThue)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (Phong is null)
            return NotFound();

        return Page();
    }
}
