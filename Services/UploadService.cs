namespace QuanLyNhaTro.Services;

public class UploadService : IUploadService
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<UploadService> _logger;

    public UploadService(IWebHostEnvironment env, ILogger<UploadService> logger)
    {
        _env = env;
        _logger = logger;
    }

    public async Task<string?> SaveFileAsync(IFormFile file, string subFolder, CancellationToken cancellationToken = default)
    {
        if (file.Length == 0) return null;

        var safeName = Path.GetFileName(file.FileName);
        var ext = Path.GetExtension(safeName);
        var unique = $"{Guid.NewGuid():N}{ext}";
        var dir = Path.Combine(_env.WebRootPath, "uploads", subFolder);
        Directory.CreateDirectory(dir);
        var physical = Path.Combine(dir, unique);

        await using (var stream = File.Create(physical))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        var relative = $"/uploads/{subFolder}/{unique}".Replace('\\', '/');
        _logger.LogInformation("Đã upload: {Path}", relative);
        return relative;
    }
}
