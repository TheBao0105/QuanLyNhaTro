namespace QuanLyNhaTro.Models;

public class ThanhToan
{
    public int Id { get; set; }
    public int HoaDonId { get; set; }
    public HoaDon HoaDon { get; set; } = null!;
    public decimal SoTien { get; set; }
    public DateTime NgayThanhToan { get; set; } = DateTime.UtcNow;
    public string? GhiChu { get; set; }
    /// <summary>TienMat, ChuyenKhoan, ViDienTu</summary>
    public string PhuongThuc { get; set; } = "TienMat";
}
