using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;

namespace QuanLyNhaTro.Pages.HopDong;

[Authorize]
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;

    public CreateModel(AppDbContext db) => _db = db;

    [BindProperty]
    public global::QuanLyNhaTro.Models.HopDong HopDong { get; set; } = new();

    public SelectList PhongSelect { get; private set; } = null!;
    public SelectList NguoiThueSelect { get; private set; } = null!;
    public int PhongCount { get; private set; }
    public int NguoiThueCount { get; private set; }

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        ApplyFormDefaults();
        await LoadLookupsAsync(cancellationToken);
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        await LoadLookupsAsync(cancellationToken);
        NormalizeDates();

        if (!ModelState.IsValid)
            return Page();

        try
        {
            _db.HopDongs.Add(HopDong);
            await _db.SaveChangesAsync(cancellationToken);

            if (HopDong.TrangThai == "HieuLuc")
            {
                var phong = await _db.Phongs.FindAsync(new object[] { HopDong.PhongId }, cancellationToken);
                if (phong is not null)
                {
                    phong.TrangThai = "DaThue";
                    await _db.SaveChangesAsync(cancellationToken);
                }
            }

            return RedirectToPage("Index");
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty,
                "Không lưu được hợp đồng (ràng buộc CSDL hoặc phòng đã có hợp đồng khác). Vui lòng thử lại hoặc chọn phòng/người thuê khác.");
            return Page();
        }
    }

    private void ApplyFormDefaults()
    {
        var today = DateTime.Today;
        HopDong.NgayBatDau = today;
        HopDong.NgayKetThuc = null;
        HopDong.TienCoc = 0;
        HopDong.TrangThai = "HieuLuc";
    }

    private void NormalizeDates()
    {
        if (HopDong.NgayBatDau.Year < 1900)
            HopDong.NgayBatDau = DateTime.Today;
    }

    private async Task LoadLookupsAsync(CancellationToken cancellationToken)
    {
        var phongs = await _db.Phongs.AsNoTracking().OrderBy(p => p.TenPhong).ToListAsync(cancellationToken);
        var nguoi = await _db.NguoiThues.AsNoTracking().OrderBy(n => n.HoTen).ToListAsync(cancellationToken);
        PhongCount = phongs.Count;
        NguoiThueCount = nguoi.Count;
        PhongSelect = new SelectList(phongs, nameof(global::QuanLyNhaTro.Models.Phong.Id), nameof(global::QuanLyNhaTro.Models.Phong.TenPhong));
        NguoiThueSelect = new SelectList(nguoi, nameof(global::QuanLyNhaTro.Models.NguoiThue.Id), nameof(global::QuanLyNhaTro.Models.NguoiThue.HoTen));
    }
}
