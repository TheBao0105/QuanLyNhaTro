namespace QuanLyNhaTro.Models;

public class TaiKhoan
{
    public int Id { get; set; }
    public string TenDangNhap { get; set; } = string.Empty;
    public string MatKhauHash { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    /// <summary>Admin, ChuTro</summary>
    public string VaiTro { get; set; } = "ChuTro";
    public bool HoatDong { get; set; } = true;
}
