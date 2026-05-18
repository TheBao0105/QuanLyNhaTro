using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.Models;

public class DichVu
{
    public int DichVuId { get; set; }

    [Required, StringLength(200)]
    [Display(Name = "Tên dịch vụ")]
    public string TenDichVu { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    [Display(Name = "Đơn giá")]
    public decimal DonGia { get; set; }

    [StringLength(50)]
    [Display(Name = "Đơn vị tính")]
    public string DonViTinh { get; set; } = "tháng";

    public bool TrangThai { get; set; } = true;

    [StringLength(500)]
    public string? GhiChu { get; set; }
}
