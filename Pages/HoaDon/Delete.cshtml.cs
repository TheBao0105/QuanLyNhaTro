using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using Entity = QuanLyNhaTro.Models.HoaDon;

namespace QuanLyNhaTro.Pages.HoaDon;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;

    public DeleteModel(AppDbContext db) => _db = db;

    public Entity? HoaDon { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        HoaDon = await _db.HoaDons
            .Include(h => h.HopDong)
            .ThenInclude(d => d!.Phong)
            .Include(h => h.HopDong)
            .ThenInclude(d => d!.NguoiThue)
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (HoaDon is null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _db.HoaDons.FindAsync(new object[] { id }, cancellationToken);
        if (entity is null)
            return NotFound();

        _db.HoaDons.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return RedirectToPage("Index");
    }
}
