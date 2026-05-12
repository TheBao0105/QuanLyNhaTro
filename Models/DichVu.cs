namespace QuanLyNhaTro.Models;

public class DichVu
{
    public int Id { get; set; }
    public string TenDichVu { get; set; } = string.Empty;
    public decimal DonGia { get; set; }
    /// <summary>kWh, m3, thang, ...</summary>
    public string DonViTinh { get; set; } = "thang";
}
