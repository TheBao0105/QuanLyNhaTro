using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace QuanLyNhaTro.Models;

public class ThanhToan
{
    public int ThanhToanId { get; set; }

    public int HoaDonId { get; set; }

    [ValidateNever]
    public HoaDon HoaDon { get; set; } = null!;

    [Range(0.01, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 0")]
    public decimal SoTien { get; set; }

    public DateTime NgayThanhToan { get; set; } = DateTime.UtcNow;

    [StringLength(50)]
    public string PhuongThuc { get; set; } = "TienMat";

    [StringLength(200)]
    public string? NguoiThu { get; set; }

    [StringLength(500)]
    public string? GhiChu { get; set; }
}
