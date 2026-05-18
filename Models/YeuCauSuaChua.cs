using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyNhaTro.Models;

public class YeuCauSuaChua
{
    public int YeuCauSuaChuaId { get; set; }

    public int PhongId { get; set; }

    [ValidateNever]
    public Phong Phong { get; set; } = null!;

    [Required, StringLength(200)]
    public string TieuDe { get; set; } = string.Empty;

    [Required, StringLength(2000)]
    public string NoiDung { get; set; } = string.Empty;

    public DateTime NgayGui { get; set; } = DateTime.UtcNow;

    [StringLength(200)]
    public string? NguoiGui { get; set; }

    [StringLength(30)]
    public string TrangThai { get; set; } = TrangThaiConstants.YeuCauSuaChua.ChoXuLy;

    [StringLength(1000)]
    public string? GhiChuXuLy { get; set; }
}
