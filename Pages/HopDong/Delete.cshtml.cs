using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;

namespace QuanLyNhaTro.Pages.HopDong;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly AppDbContext _db;

    public DeleteModel(AppDbContext db) => _db = db;

    public global::QuanLyNhaTro.Models.HopDong? HopDong { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        HopDong = await _db.HopDongs
            .Include(h => h.Phong)
            .Include(h => h.NguoiThue)
            .FirstOrDefaultAsync(h => h.HopDongId == id, cancellationToken);

        if (HopDong is null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _db.HopDongs.FindAsync(new object[] { id }, cancellationToken);
        if (entity is null)
            return NotFound();

        _db.HopDongs.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);
        return RedirectToPage("Index");
    }
}
