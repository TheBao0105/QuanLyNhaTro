using Microsoft.AspNetCore.Mvc;

namespace QuanLyNhaTro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthApiController : ControllerBase
{
    [HttpGet]
    [Produces("application/json")]
    public IActionResult Get() => Ok(new { ok = true, app = "QuanLyNhaTro", time = DateTime.UtcNow });
}
