namespace QuanLyNhaTro.ViewModels;

public class DashboardVM
{
    public int TongPhong { get; set; }
    public int PhongTrong { get; set; }
    public int PhongDangThue { get; set; }
    public int PhongBaoTri { get; set; }
    public int TongNguoiThue { get; set; }
    public int HopDongHieuLuc { get; set; }
    public int HoaDonChuaThu { get; set; }
    public decimal TongNo { get; set; }
    public decimal DoanhThuThang { get; set; }
    public decimal DoanhThuNam { get; set; }
    public List<HopDongSapHetHanItem> HopDongSapHetHan { get; set; } = new();
    public List<HoaDonQuaHanItem> HoaDonQuaHan { get; set; } = new();
}

public class HopDongSapHetHanItem
{
    public int HopDongId { get; set; }
    public string TenPhong { get; set; } = "";
    public string TenNguoiThue { get; set; } = "";
    public DateTime? NgayKetThuc { get; set; }
}

public class HoaDonQuaHanItem
{
    public int HoaDonId { get; set; }
    public string TenPhong { get; set; } = "";
    public int Thang { get; set; }
    public int Nam { get; set; }
    public decimal ConNo { get; set; }
}
