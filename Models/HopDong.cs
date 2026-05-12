using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyNhaTro.Models;

public class HopDong
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Chọn phòng")]
    public int PhongId { get; set; }

    [ValidateNever]
    public Phong Phong { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "Chọn người thuê")]
    public int NguoiThueId { get; set; }

    [ValidateNever]
    public NguoiThue NguoiThue { get; set; } = null!;
    public DateTime NgayBatDau { get; set; }
    public DateTime? NgayKetThuc { get; set; }
    public decimal TienCoc { get; set; }
    /// <summary>HieuLuc, KetThuc, Huy</summary>
    public string TrangThai { get; set; } = "HieuLuc";

    [ValidateNever]
    public ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}
