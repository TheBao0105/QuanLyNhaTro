using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Models;

namespace QuanLyNhaTro.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Phong> Phongs => Set<Phong>();
    public DbSet<NguoiThue> NguoiThues => Set<NguoiThue>();
    public DbSet<HopDong> HopDongs => Set<HopDong>();
    public DbSet<HoaDon> HoaDons => Set<HoaDon>();
    public DbSet<TaiKhoan> TaiKhoans => Set<TaiKhoan>();
    public DbSet<DichVu> DichVus => Set<DichVu>();
    public DbSet<ThanhToan> ThanhToans => Set<ThanhToan>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Phong>(e =>
        {
            e.Property(p => p.TenPhong).HasMaxLength(200);
            e.Property(p => p.TrangThai).HasMaxLength(50);
            e.Property(p => p.GiaThue).HasPrecision(18, 2);
        });

        modelBuilder.Entity<NguoiThue>(e =>
        {
            e.Property(n => n.HoTen).HasMaxLength(200);
            e.Property(n => n.Cccd).HasMaxLength(20);
            e.Property(n => n.SoDienThoai).HasMaxLength(20);
            e.Property(n => n.Email).HasMaxLength(200);
        });

        modelBuilder.Entity<HopDong>(e =>
        {
            e.Property(h => h.TrangThai).HasMaxLength(50);
            e.Property(h => h.TienCoc).HasPrecision(18, 2);
            e.HasOne(h => h.Phong).WithMany(p => p.HopDongs).HasForeignKey(h => h.PhongId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(h => h.NguoiThue).WithMany(n => n.HopDongs).HasForeignKey(h => h.NguoiThueId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<HoaDon>(e =>
        {
            e.Property(h => h.TrangThai).HasMaxLength(50);
            e.Property(h => h.TienPhong).HasPrecision(18, 2);
            e.Property(h => h.TienDien).HasPrecision(18, 2);
            e.Property(h => h.TienNuoc).HasPrecision(18, 2);
            e.Property(h => h.TienDichVu).HasPrecision(18, 2);
            e.Property(h => h.TongTien).HasPrecision(18, 2);
            e.HasIndex(h => new { h.HopDongId, h.Thang, h.Nam }).IsUnique();
            e.HasOne(h => h.HopDong).WithMany(d => d.HoaDons).HasForeignKey(h => h.HopDongId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TaiKhoan>(e =>
        {
            e.HasIndex(t => t.TenDangNhap).IsUnique();
            e.Property(t => t.TenDangNhap).HasMaxLength(100);
            e.Property(t => t.HoTen).HasMaxLength(200);
            e.Property(t => t.VaiTro).HasMaxLength(50);
        });

        modelBuilder.Entity<DichVu>(e =>
        {
            e.Property(d => d.TenDichVu).HasMaxLength(200);
            e.Property(d => d.DonViTinh).HasMaxLength(50);
            e.Property(d => d.DonGia).HasPrecision(18, 2);
        });

        modelBuilder.Entity<ThanhToan>(e =>
        {
            e.Property(t => t.SoTien).HasPrecision(18, 2);
            e.Property(t => t.PhuongThuc).HasMaxLength(50);
            e.HasOne(t => t.HoaDon).WithMany(h => h.ThanhToans).HasForeignKey(t => t.HoaDonId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}
