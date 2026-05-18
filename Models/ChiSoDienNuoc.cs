using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyNhaTro.Models;

public class ChiSoDienNuoc
{
    public int ChiSoDienNuocId { get; set; }

    [Range(1, int.MaxValue)]
    public int PhongId { get; set; }

    [ValidateNever]
    public Phong Phong { get; set; } = null!;

    [Range(1, 12)]
    public int Thang { get; set; }

    [Range(2000, 2100)]
    public int Nam { get; set; }

    [Range(0, int.MaxValue)]
    [Display(Name = "Chỉ số điện cũ")]
    public int ChiSoDienCu { get; set; }

    [Range(0, int.MaxValue)]
    [Display(Name = "Chỉ số điện mới")]
    public int ChiSoDienMoi { get; set; }

    [Range(0, int.MaxValue)]
    [Display(Name = "Chỉ số nước cũ")]
    public int ChiSoNuocCu { get; set; }

    [Range(0, int.MaxValue)]
    [Display(Name = "Chỉ số nước mới")]
    public int ChiSoNuocMoi { get; set; }

    [Range(0, double.MaxValue)]
    public decimal DonGiaDien { get; set; }

    [Range(0, double.MaxValue)]
    public decimal DonGiaNuoc { get; set; }

    public DateTime NgayNhap { get; set; } = DateTime.UtcNow;

    [NotMapped]
    public decimal TienDien => (ChiSoDienMoi - ChiSoDienCu) * DonGiaDien;

    [NotMapped]
    public decimal TienNuoc => (ChiSoNuocMoi - ChiSoNuocCu) * DonGiaNuoc;
}
