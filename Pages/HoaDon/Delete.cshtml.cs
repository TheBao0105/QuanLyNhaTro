using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;

namespace QuanLyNhaTro.Pages.HoaDon;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;

    public DeleteModel(AppDbContext db) => _db = db;

    public HoaDonEntity? Invoice { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        Invoice = await _db.HoaDons
            .Include(h => h.Phong)
            .FirstOrDefaultAsync(h => h.HoaDonId == id, cancellationToken);

        if (Invoice is null)
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
