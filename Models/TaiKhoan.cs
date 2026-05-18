using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.Models;

public class TaiKhoan
{
    public int TaiKhoanId { get; set; }

    [Required, StringLength(200)]
    public string HoTen { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string TenDangNhap { get; set; } = string.Empty;

    /// <summary>Hash BCrypt — không map cột MatKhau plain text.</summary>
    [Required]
    public string MatKhauHash { get; set; } = string.Empty;

    [StringLength(30)]
    public string VaiTro { get; set; } = TrangThaiConstants.VaiTro.ChuTro;

    public bool TrangThai { get; set; } = true;

    public DateTime NgayTao { get; set; } = DateTime.UtcNow;
}
