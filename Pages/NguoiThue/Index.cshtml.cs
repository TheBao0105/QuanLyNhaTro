using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaTro.Repositories;

namespace QuanLyNhaTro.Pages.NguoiThue;

[Authorize]
public class IndexModel : PageModel
{
    private readonly INguoiThueRepository _repo;

    public IndexModel(INguoiThueRepository repo) => _repo = repo;

    public IReadOnlyList<global::QuanLyNhaTro.Models.NguoiThue> Items { get; private set; } = Array.Empty<global::QuanLyNhaTro.Models.NguoiThue>();

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Items = await _repo.GetAllAsync(cancellationToken);
    }
}
