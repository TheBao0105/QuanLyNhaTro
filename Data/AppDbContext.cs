using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Phong> Phongs => Set<Phong>();
    public DbSet<NguoiThue> NguoiThues => Set<NguoiThue>();
    public DbSet<HopDong> HopDongs => Set<HopDong>();
    public DbSet<DichVu> DichVus => Set<DichVu>();
    public DbSet<ChiSoDienNuoc> ChiSoDienNuocs => Set<ChiSoDienNuoc>();
    public DbSet<HoaDon> HoaDons => Set<HoaDon>();
    public DbSet<ChiTietHoaDon> ChiTietHoaDons => Set<ChiTietHoaDon>();
    public DbSet<ThanhToan> ThanhToans => Set<ThanhToan>();
    public DbSet<TaiKhoan> TaiKhoans => Set<TaiKhoan>();
    public DbSet<YeuCauSuaChua> YeuCauSuaChuas => Set<YeuCauSuaChua>();
    public DbSet<ThongBao> ThongBaos => Set<ThongBao>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Phong>(e =>
        {
            e.HasKey(p => p.PhongId);
            e.Property(p => p.GiaThue).HasPrecision(18, 2);
            e.Property(p => p.DienTich).HasPrecision(10, 2);
        });

        modelBuilder.Entity<NguoiThue>(e =>
        {
            e.HasKey(n => n.NguoiThueId);
            e.HasIndex(n => n.CCCD).IsUnique().HasFilter("[CCCD] IS NOT NULL AND [CCCD] <> ''");
        });

        modelBuilder.Entity<HopDong>(e =>
        {
            e.HasKey(h => h.HopDongId);
            e.Property(h => h.TienCoc).HasPrecision(18, 2);
            e.Property(h => h.GiaThue).HasPrecision(18, 2);
            e.HasOne(h => h.Phong).WithMany(p => p.HopDongs).HasForeignKey(h => h.PhongId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(h => h.NguoiThue).WithMany(n => n.HopDongs).HasForeignKey(h => h.NguoiThueId).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(h => h.PhongId).HasFilter($"[TrangThai] = '{TrangThaiConstants.HopDong.HieuLuc}'").IsUnique();
        });

        modelBuilder.Entity<DichVu>(e =>
        {
            e.HasKey(d => d.DichVuId);
            e.Property(d => d.DonGia).HasPrecision(18, 2);
        });

        modelBuilder.Entity<ChiSoDienNuoc>(e =>
        {
            e.HasKey(c => c.ChiSoDienNuocId);
            e.Property(c => c.DonGiaDien).HasPrecision(18, 2);
            e.Property(c => c.DonGiaNuoc).HasPrecision(18, 2);
            e.HasIndex(c => new { c.PhongId, c.Thang, c.Nam }).IsUnique();
            e.HasOne(c => c.Phong).WithMany(p => p.ChiSoDienNuocs).HasForeignKey(c => c.PhongId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<HoaDon>(e =>
        {
            e.HasKey(h => h.HoaDonId);
            e.Property(h => h.TienPhong).HasPrecision(18, 2);
            e.Property(h => h.TienDien).HasPrecision(18, 2);
            e.Property(h => h.TienNuoc).HasPrecision(18, 2);
            e.Property(h => h.TienDichVu).HasPrecision(18, 2);
            e.Property(h => h.TongTien).HasPrecision(18, 2);
            e.Property(h => h.DaThanhToan).HasPrecision(18, 2);
            e.Property(h => h.ConNo).HasPrecision(18, 2);
            e.HasIndex(h => new { h.PhongId, h.Thang, h.Nam }).IsUnique();
            e.HasOne(h => h.Phong).WithMany(p => p.HoaDons).HasForeignKey(h => h.PhongId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ChiTietHoaDon>(e =>
        {
            e.HasKey(c => c.ChiTietHoaDonId);
            e.Property(c => c.SoLuong).HasPrecision(18, 2);
            e.Property(c => c.DonGia).HasPrecision(18, 2);
            e.Property(c => c.ThanhTien).HasPrecision(18, 2);
            e.HasOne(c => c.HoaDon).WithMany(h => h.ChiTietHoaDons).HasForeignKey(c => c.HoaDonId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ThanhToan>(e =>
        {
            e.HasKey(t => t.ThanhToanId);
            e.Property(t => t.SoTien).HasPrecision(18, 2);
            e.HasOne(t => t.HoaDon).WithMany(h => h.ThanhToans).HasForeignKey(t => t.HoaDonId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TaiKhoan>(e =>
        {
            e.HasKey(t => t.TaiKhoanId);
            e.HasIndex(t => t.TenDangNhap).IsUnique();
        });

        modelBuilder.Entity<YeuCauSuaChua>(e =>
        {
            e.HasKey(y => y.YeuCauSuaChuaId);
            e.HasOne(y => y.Phong).WithMany(p => p.YeuCauSuaChuas).HasForeignKey(y => y.PhongId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ThongBao>(e => e.HasKey(t => t.ThongBaoId));
    }
}
