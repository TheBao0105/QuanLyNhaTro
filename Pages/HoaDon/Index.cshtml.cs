using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaTro.Repositories;

namespace QuanLyNhaTro.Pages.HoaDon;

[Authorize(Policy = "QuanLy")]
public class IndexModel : PageModel
{
    private readonly IHoaDonRepository _repo;

    public IndexModel(IHoaDonRepository repo) => _repo = repo;

    public IReadOnlyList<HoaDonEntity> Items { get; private set; } = Array.Empty<HoaDonEntity>();

    public int? Thang { get; set; }
    public int? Nam { get; set; }
    public string? TrangThai { get; set; }

    public async Task OnGetAsync(int? thang, int? nam, string? trangThai, CancellationToken ct)
    {
        Thang = thang; Nam = nam; TrangThai = trangThai;
        Items = await _repo.GetAllAsync(thang, nam, trangThai, ct);
    }
}
