using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Repositories;

namespace QuanLyNhaTro.Pages.NguoiThue;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly INguoiThueRepository _repo;
    private readonly AppDbContext _db;

    public DeleteModel(INguoiThueRepository repo, AppDbContext db)
    {
        _repo = repo;
        _db = db;
    }

    public global::QuanLyNhaTro.Models.NguoiThue? NguoiThue { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        NguoiThue = await _repo.GetByIdAsync(id, cancellationToken);
        if (NguoiThue is null)
            return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
    {
        if (await _db.HopDongs.AnyAsync(h => h.NguoiThueId == id, cancellationToken))
        {
            ModelState.AddModelError(string.Empty, "Không xóa được: người thuê đang có hợp đồng.");
            NguoiThue = await _repo.GetByIdAsync(id, cancellationToken);
            return Page();
        }

        await _repo.DeleteAsync(id, cancellationToken);
        return RedirectToPage("Index");
    }
}
