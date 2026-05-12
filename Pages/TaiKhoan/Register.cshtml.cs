using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuanLyNhaTro.Data;

namespace QuanLyNhaTro.Pages.TaiKhoan;

[AllowAnonymous]
public class RegisterModel : PageModel
{
    private readonly AppDbContext _db;

    public RegisterModel(AppDbContext db) => _db = db;

    [BindProperty]
    [Required(ErrorMessage = "Bắt buộc")]
    [StringLength(100)]
    public string TenDangNhap { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Bắt buộc")]
    [StringLength(200)]
    public string HoTen { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Bắt buộc")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Tối thiểu 6 ký tự")]
    public string MatKhau { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "Bắt buộc")]
    [DataType(DataType.Password)]
    [Compare(nameof(MatKhau), ErrorMessage = "Mật khẩu nhập lại không khớp")]
    public string MatKhauNhapLai { get; set; } = string.Empty;

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync(CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return Page();

        if (await _db.TaiKhoans.AnyAsync(t => t.TenDangNhap == TenDangNhap, cancellationToken))
        {
            ModelState.AddModelError(nameof(TenDangNhap), "Tên đăng nhập đã tồn tại.");
            return Page();
        }

        var user = new global::QuanLyNhaTro.Models.TaiKhoan
        {
            TenDangNhap = TenDangNhap.Trim(),
            HoTen = HoTen.Trim(),
            MatKhauHash = BCrypt.Net.BCrypt.HashPassword(MatKhau),
            VaiTro = "ChuTro",
            HoatDong = true
        };

        _db.TaiKhoans.Add(user);
        await _db.SaveChangesAsync(cancellationToken);

        return RedirectToPage("Login");
    }
}
