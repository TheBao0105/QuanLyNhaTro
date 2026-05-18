using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuanLyNhaTro.Services;
using QuanLyNhaTro.ViewModels;

namespace QuanLyNhaTro.Pages.Dashboard;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ThongKeService _thongKe;

    public IndexModel(ThongKeService thongKe) => _thongKe = thongKe;

    public DashboardVM Stats { get; private set; } = new();

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Stats = await _thongKe.LayDashboardAsync(cancellationToken);
    }
}
