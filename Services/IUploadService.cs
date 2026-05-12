namespace QuanLyNhaTro.Services;

public interface IUploadService
{
    /// <summary>Lưu file vào wwwroot/uploads, trả về đường dẫn tương đối (bắt đầu bằng /).</summary>
    Task<string?> SaveFileAsync(IFormFile file, string subFolder, CancellationToken cancellationToken = default);
}
