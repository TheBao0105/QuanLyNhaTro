using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyNhaTro.Models;

public class HoaDon
{
    public int Id { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Chọn hợp đồng")]
    public int HopDongId { get; set; }

    [ValidateNever]
    public HopDong HopDong { get; set; } = null!;
    [Range(1, 12)]
    public int Thang { get; set; }

    [Range(2000, 2100)]
    public int Nam { get; set; }
    public decimal TienPhong { get; set; }
    public decimal TienDien { get; set; }
    public decimal TienNuoc { get; set; }
    public decimal TienDichVu { get; set; }
    public decimal TongTien { get; set; }
    /// <summary>ChuaThanhToan, DaThanhToan, MotPhan</summary>
    public string TrangThai { get; set; } = "ChuaThanhToan";
    public DateTime NgayLap { get; set; } = DateTime.UtcNow;

    [ValidateNever]
    public ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
