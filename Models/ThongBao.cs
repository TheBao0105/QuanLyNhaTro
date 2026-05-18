using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.Models;

public class ThongBao
{
    public int ThongBaoId { get; set; }

    [Required, StringLength(200)]
    public string TieuDe { get; set; } = string.Empty;

    [Required, StringLength(4000)]
    public string NoiDung { get; set; } = string.Empty;

    public DateTime NgayTao { get; set; } = DateTime.UtcNow;

    [StringLength(100)]
    public string DoiTuongNhan { get; set; } = "TatCa";

    public bool DaDoc { get; set; }
}
