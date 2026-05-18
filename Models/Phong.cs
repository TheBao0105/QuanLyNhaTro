using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyNhaTro.Models;

public class Phong
{
    public int PhongId { get; set; }

    [Required, StringLength(50)]
    [Display(Name = "Tên phòng")]
    public string TenPhong { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    [Display(Name = "Giá thuê")]
    public decimal GiaThue { get; set; }

    [Range(1, 10000)]
    [Display(Name = "Diện tích (m²)")]
    public decimal DienTich { get; set; }

    [Range(1, 50)]
    [Display(Name = "Số người tối đa")]
    public int SoNguoiToiDa { get; set; } = 2;

    [StringLength(30)]
    [Display(Name = "Trạng thái")]
    public string TrangThai { get; set; } = TrangThaiConstants.Phong.ConTrong;

    [StringLength(500)]
    public string? MoTa { get; set; }

    [StringLength(500)]
    public string? HinhAnh { get; set; }

    public DateTime NgayTao { get; set; } = DateTime.UtcNow;

    [ValidateNever]
    public ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();

    [ValidateNever]
    public ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    [ValidateNever]
    public ICollection<ChiSoDienNuoc> ChiSoDienNuocs { get; set; } = new List<ChiSoDienNuoc>();

    [ValidateNever]
    public ICollection<YeuCauSuaChua> YeuCauSuaChuas { get; set; } = new List<YeuCauSuaChua>();
}
