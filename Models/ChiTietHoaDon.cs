using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyNhaTro.Models;

public class ChiTietHoaDon
{
    public int ChiTietHoaDonId { get; set; }

    public int HoaDonId { get; set; }

    [ValidateNever]
    public HoaDon HoaDon { get; set; } = null!;

    [Required, StringLength(200)]
    [Display(Name = "Khoản thu")]
    public string TenKhoanThu { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal SoLuong { get; set; } = 1;

    [Range(0, double.MaxValue)]
    public decimal DonGia { get; set; }

    [Range(0, double.MaxValue)]
    public decimal ThanhTien { get; set; }
}
