using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyNhaTro.Models;

public class HoaDon
{
    public int HoaDonId { get; set; }

    [Range(1, int.MaxValue)]
    public int PhongId { get; set; }

    [ValidateNever]
    public Phong Phong { get; set; } = null!;

    [Range(1, 12)]
    public int Thang { get; set; }

    [Range(2000, 2100)]
    public int Nam { get; set; }

    [Range(0, double.MaxValue)]
    public decimal TienPhong { get; set; }

    [Range(0, double.MaxValue)]
    public decimal TienDien { get; set; }

    [Range(0, double.MaxValue)]
    public decimal TienNuoc { get; set; }

    [Range(0, double.MaxValue)]
    public decimal TienDichVu { get; set; }

    [Range(0, double.MaxValue)]
    public decimal TongTien { get; set; }

    [Range(0, double.MaxValue)]
    public decimal DaThanhToan { get; set; }

    [Range(0, double.MaxValue)]
    public decimal ConNo { get; set; }

    public DateTime NgayLap { get; set; } = DateTime.UtcNow;

    [DataType(DataType.Date)]
    public DateTime? HanThanhToan { get; set; }

    [StringLength(30)]
    public string TrangThai { get; set; } = TrangThaiConstants.HoaDon.ChuaThanhToan;

    [ValidateNever]
    public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    [ValidateNever]
    public ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
