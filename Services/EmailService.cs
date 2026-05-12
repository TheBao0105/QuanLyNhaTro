namespace QuanLyNhaTro.Services;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger) => _logger = logger;

    public Task SendAsync(string to, string subject, string body, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("[Email stub] To={To} Subject={Subject}", to, subject);
        return Task.CompletedTask;
    }
}
