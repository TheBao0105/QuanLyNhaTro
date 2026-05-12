using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaTro.Repositories;

namespace QuanLyNhaTro.Pages.Phong;

[Authorize]
public class CreateModel : PageModel
{
    private readonly IPhongRepository _repo;

    public CreateModel(IPhongRepository repo) => _repo = repo;

    [BindProperty]
    public global::QuanLyNhaTro.Models.Phong Phong { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return Page();

        await _repo.AddAsync(Phong, cancellationToken);
        return RedirectToPage("Index");
    }
}
