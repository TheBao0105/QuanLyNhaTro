using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Pages.DichVu;

[Authorize(Policy = "QuanLy")]
public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    public EditModel(AppDbContext db) => _db = db;
    [BindProperty] public global::QuanLyNhaTro.Models.DichVu DichVu { get; set; } = new();
    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var e = await _db.DichVus.FindAsync(new object[] { id }, ct);
        if (e is null) return NotFound();
        DichVu = e; return Page();
    }
    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid) return Page();
        _db.DichVus.Update(DichVu);
        await _db.SaveChangesAsync(ct);
        return RedirectToPage("Index");
    }
}
