using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.Models;

public class Phong
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Bắt buộc")]
    [StringLength(200)]
    public string TenPhong { get; set; } = string.Empty;
    public string? MoTa { get; set; }
    [Range(0, 1_000_000_000)]
    public decimal GiaThue { get; set; }

    [Range(1, 10000)]
    public int DienTichM2 { get; set; }
    /// <summary>Trong, DaThue, BaoTri</summary>
    public string TrangThai { get; set; } = "Trong";

    public ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();
}
