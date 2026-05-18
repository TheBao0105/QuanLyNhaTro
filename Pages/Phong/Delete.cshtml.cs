using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Repositories;

namespace QuanLyNhaTro.Pages.Phong;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly IPhongRepository _repo;
    private readonly AppDbContext _db;

    public DeleteModel(IPhongRepository repo, AppDbContext db)
    {
        _repo = repo;
        _db = db;
    }

    public global::QuanLyNhaTro.Models.Phong? Phong { get; private set; }

    public async Task<IActionResult> OnGetAsync(int id, CancellationToken cancellationToken)
    {
        Phong = await _repo.GetByIdAsync(id, cancellationToken);
        if (Phong is null)
            return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id, CancellationToken cancellationToken)
    {
        if (await _db.HopDongs.AnyAsync(h => h.PhongId == id && h.TrangThai == Models.TrangThaiConstants.HopDong.HieuLuc, cancellationToken))
        {
            ModelState.AddModelError(string.Empty, "Không xóa được: phòng đang có hợp đồng hiệu lực.");
            Phong = await _repo.GetByIdAsync(id, cancellationToken);
            return Page();
        }

        await _repo.DeleteAsync(id, cancellationToken);
        return RedirectToPage("Index");
    }
}
