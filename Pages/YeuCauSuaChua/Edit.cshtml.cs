using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Pages.YeuCauSuaChua;

[Authorize(Policy = "QuanLy")]
public class EditModel : PageModel
{
    private readonly AppDbContext _db;
    public EditModel(AppDbContext db) => _db = db;
    [BindProperty] public global::QuanLyNhaTro.Models.YeuCauSuaChua YeuCau { get; set; } = new();
    public async Task<IActionResult> OnGetAsync(int id, CancellationToken ct)
    {
        var e = await _db.YeuCauSuaChuas.FindAsync(new object[] { id }, ct);
        if (e is null) return NotFound();
        YeuCau = e; return Page();
    }
    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid) return Page();
        _db.YeuCauSuaChuas.Update(YeuCau);
        await _db.SaveChangesAsync(ct);
        return RedirectToPage("Index");
    }
}
