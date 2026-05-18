using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;
using QuanLyNhaTro.Models;
using QuanLyNhaTro.Repositories;

namespace QuanLyNhaTro.Pages.Phong;

[Authorize(Policy = "QuanLy")]
public class IndexModel : PageModel
{
    private readonly IPhongRepository _repo;
    private readonly AppDbContext _db;

    public IndexModel(IPhongRepository repo, AppDbContext db) { _repo = repo; _db = db; }

    public IReadOnlyList<global::QuanLyNhaTro.Models.Phong> Items { get; private set; } = Array.Empty<global::QuanLyNhaTro.Models.Phong>();

    [BindProperty(SupportsGet = true)]
    public string? Q { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? TrangThai { get; set; }

    public async Task OnGetAsync(CancellationToken ct) =>
        Items = await _repo.GetAllAsync(Q, TrangThai, ct);

    public async Task<IActionResult> OnPostDeleteAsync(int id, CancellationToken ct)
    {
        if (await _db.HopDongs.AnyAsync(h => h.PhongId == id && h.TrangThai == TrangThaiConstants.HopDong.HieuLuc, ct))
        {
            TempData["Error"] = "Không xóa được: phòng đang có hợp đồng hiệu lực.";
            return RedirectToPage();
        }
        await _repo.DeleteAsync(id, ct);
        TempData["Success"] = "Đã xóa phòng.";
        return RedirectToPage();
    }
}
