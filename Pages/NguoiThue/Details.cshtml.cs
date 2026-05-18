using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;

namespace QuanLyNhaTro.Pages.NguoiThue;

[Authorize]
public class DetailsModel : PageModel
{
    private readonly AppDbContext _db;

    public DetailsModel(AppDbContext db) => _db = db;

    public global::QuanLyNhaTro.Models.NguoiThue? NguoiThue { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        NguoiThue = await _db.NguoiThues
            .AsNoTracking()
            .Include(n => n.HopDongs)
            .ThenInclude(h => h.Phong)
            .FirstOrDefaultAsync(n => n.NguoiThueId == id, cancellationToken);

        if (NguoiThue is null)
            return NotFound();

        return Page();
    }
}
