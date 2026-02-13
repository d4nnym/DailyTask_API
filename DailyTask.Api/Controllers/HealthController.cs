using DailyTask.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DailyTask.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    [HttpGet("db")]
    public async Task<IActionResult> Db([FromServices] DailyTaskDbContext db)
    {
        var ok = await db.Database.CanConnectAsync();
        return Ok(new { ok });
    }
}