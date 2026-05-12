using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaTro.Repositories;

namespace QuanLyNhaTro.Pages.Phong;

[Authorize]
public class IndexModel : PageModel
{
    private readonly IPhongRepository _repo;

    public IndexModel(IPhongRepository repo) => _repo = repo;

    public IReadOnlyList<global::QuanLyNhaTro.Models.Phong> Items { get; private set; } = Array.Empty<global::QuanLyNhaTro.Models.Phong>();

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Items = await _repo.GetAllAsync(cancellationToken);
    }
}
