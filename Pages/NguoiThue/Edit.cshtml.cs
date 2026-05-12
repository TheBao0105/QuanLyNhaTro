using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaTro.Repositories;

namespace QuanLyNhaTro.Pages.NguoiThue;

[Authorize]
public class EditModel : PageModel
{
    private readonly INguoiThueRepository _repo;

    public EditModel(INguoiThueRepository repo) => _repo = repo;

    [BindProperty]
    public global::QuanLyNhaTro.Models.NguoiThue NguoiThue { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(id, cancellationToken);
        if (entity is null)
            return NotFound();
        NguoiThue = entity;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return Page();

        await _repo.UpdateAsync(NguoiThue, cancellationToken);
        return RedirectToPage("Index");
    }
}
