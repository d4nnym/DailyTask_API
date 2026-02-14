using DailyTask.App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyTask.Api.Controllers;

[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    [HttpPatch("{taskId:guid}/complete")]
    public async Task<IActionResult> Complete(
        Guid taskId,
        [FromServices] ITaskService service,
        CancellationToken ct)
        => await service.MarkDoneAsync(taskId, ct) ? NoContent() : NotFound();

    [HttpDelete("{taskId:guid}")]
    public async Task<IActionResult> Delete(
        Guid taskId,
        [FromServices] ITaskService service,
        CancellationToken ct)
        => await service.DeleteAsync(taskId, ct) ? NoContent() : NotFound();
}
