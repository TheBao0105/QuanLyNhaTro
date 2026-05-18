using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyNhaTro.Models;

public class NguoiThue
{
    public int NguoiThueId { get; set; }

    [Required, StringLength(200)]
    [Display(Name = "Họ tên")]
    public string HoTen { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    [Display(Name = "Ngày sinh")]
    public DateTime? NgaySinh { get; set; }

    [StringLength(10)]
    [Display(Name = "Giới tính")]
    public string? GioiTinh { get; set; }

    [StringLength(20)]
    [Display(Name = "Số điện thoại")]
    public string? SoDienThoai { get; set; }

    [EmailAddress, StringLength(200)]
    public string? Email { get; set; }

    [StringLength(20)]
    public string? CCCD { get; set; }

    [StringLength(300)]
    [Display(Name = "Địa chỉ")]
    public string? DiaChi { get; set; }

    [StringLength(500)]
    public string? AnhGiayTo { get; set; }

    [StringLength(500)]
    public string? GhiChu { get; set; }

    [ValidateNever]
    public ICollection<HopDong> HopDongs { get; set; } = new List<HopDong>();
}
