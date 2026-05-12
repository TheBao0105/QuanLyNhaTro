using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.Models;

public class NguoiThue
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Bắt buộc")]
    [StringLength(200)]
    public string HoTen { get; set; } = string.Empty;
    public string? Cccd { get; set; }
    public string? SoDienThoai { get; set; }
    public string? Email { get; set; }
    public string? DiaChi { get; set; }

    public ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();
}
