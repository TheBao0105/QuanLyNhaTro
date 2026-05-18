using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyNhaTro.Models;

public class HopDong
{
    public int HopDongId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Chọn phòng")]
    [Display(Name = "Phòng")]
    public int PhongId { get; set; }

    [ValidateNever]
    public Phong Phong { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "Chọn người thuê")]
    [Display(Name = "Người thuê")]
    public int NguoiThueId { get; set; }

    [ValidateNever]
    public NguoiThue NguoiThue { get; set; } = null!;

    [Required, DataType(DataType.Date)]
    [Display(Name = "Ngày bắt đầu")]
    public DateTime NgayBatDau { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Ngày kết thúc")]
    public DateTime? NgayKetThuc { get; set; }

    [Range(0, double.MaxValue)]
    [Display(Name = "Tiền cọc")]
    public decimal TienCoc { get; set; }

    [Range(0, double.MaxValue)]
    [Display(Name = "Giá thuê")]
    public decimal GiaThue { get; set; }

    [StringLength(2000)]
    public string? NoiDung { get; set; }

    [StringLength(30)]
    public string TrangThai { get; set; } = TrangThaiConstants.HopDong.HieuLuc;
}
