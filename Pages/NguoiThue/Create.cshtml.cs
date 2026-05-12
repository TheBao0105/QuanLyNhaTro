using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaTro.Repositories;

namespace QuanLyNhaTro.Pages.NguoiThue;

[Authorize]
public class CreateModel : PageModel
{
    private readonly INguoiThueRepository _repo;

    public CreateModel(INguoiThueRepository repo) => _repo = repo;

    [BindProperty]
    public global::QuanLyNhaTro.Models.NguoiThue NguoiThue { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return Page();

        await _repo.AddAsync(NguoiThue, cancellationToken);
        return RedirectToPage("Index");
    }
}
