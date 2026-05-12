using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaTro.Repositories;

namespace QuanLyNhaTro.Pages.Phong;

[Authorize]
public class EditModel : PageModel
{
    private readonly IPhongRepository _repo;

    public EditModel(IPhongRepository repo) => _repo = repo;

    [BindProperty]
    public global::QuanLyNhaTro.Models.Phong Phong { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return NotFound();
        Phong = entity;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return Page();

        await _repo.UpdateAsync(Phong, cancellationToken);
        return RedirectToPage("Index");
    }
}
