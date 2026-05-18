using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Pages.DichVu;

[Authorize(Policy = "QuanLy")]
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    public CreateModel(AppDbContext db) => _db = db;
    [BindProperty] public global::QuanLyNhaTro.Models.DichVu DichVu { get; set; } = new();
    public void OnGet() => DichVu.TrangThai = true;
    public async Task<IActionResult> OnPostAsync(CancellationToken ct)
    {
        if (!ModelState.IsValid) return Page();
        _db.DichVus.Add(DichVu);
        await _db.SaveChangesAsync(ct);
        return RedirectToPage("Index");
    }
}
