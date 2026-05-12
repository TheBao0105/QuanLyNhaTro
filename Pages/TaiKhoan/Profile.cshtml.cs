using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;

namespace QuanLyNhaTro.Pages.TaiKhoan;

[Authorize]
public class ProfileModel : PageModel
{
    private readonly AppDbContext _db;

    public ProfileModel(AppDbContext db) => _db = db;

    public global::QuanLyNhaTro.Models.TaiKhoan? TaiKhoan { get; private set; }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (int.TryParse(idStr, out var id))
        {
            TaiKhoan = await _db.TaiKhoans.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }
    }
}
