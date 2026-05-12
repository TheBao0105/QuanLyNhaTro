using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.ViewModels;

public class HoaDonVM
{
    public int Id { get; set; }

    [Display(Name = "Hợp đồng")]
    public int HopDongId { get; set; }

    [Display(Name = "Tháng")]
    [Range(1, 12)]
    public int Thang { get; set; } = DateTime.UtcNow.Month;

    [Display(Name = "Năm")]
    [Range(2000, 2100)]
    public int Nam { get; set; } = DateTime.UtcNow.Year;

    [Display(Name = "Tiền phòng")]
    public decimal TienPhong { get; set; }

    [Display(Name = "Tiền điện")]
    public decimal TienDien { get; set; }

    [Display(Name = "Tiền nước")]
    public decimal TienNuoc { get; set; }

    [Display(Name = "Dịch vụ khác")]
    public decimal TienDichVu { get; set; }

    [Display(Name = "Tổng")]
    public decimal TongTien { get; set; }

    [Display(Name = "Trạng thái")]
    public string TrangThai { get; set; } = "ChuaThanhToan";

    public string? TenPhong { get; set; }
    public string? TenNguoiThue { get; set; }
}
