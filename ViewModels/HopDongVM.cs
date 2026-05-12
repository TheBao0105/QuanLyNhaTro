using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaTro.ViewModels;

public class HopDongVM
{
    public int Id { get; set; }

    [Display(Name = "Phòng")]
    public int PhongId { get; set; }

    [Display(Name = "Người thuê")]
    public int NguoiThueId { get; set; }

    [Display(Name = "Ngày bắt đầu")]
    [DataType(DataType.Date)]
    public DateTime NgayBatDau { get; set; } = DateTime.Today;

    [Display(Name = "Ngày kết thúc")]
    [DataType(DataType.Date)]
    public DateTime? NgayKetThuc { get; set; }

    [Display(Name = "Tiền cọc")]
    public decimal TienCoc { get; set; }

    [Display(Name = "Trạng thái")]
    public string TrangThai { get; set; } = "HieuLuc";

    public string? TenPhong { get; set; }
    public string? TenNguoiThue { get; set; }
}
