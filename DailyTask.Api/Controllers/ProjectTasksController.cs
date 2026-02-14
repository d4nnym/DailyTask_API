using DailyTask.App.DTOs.Tasks;
using DailyTask.App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyTask.Api.Controllers;

[ApiController]
[Route("api/projects/{projectId:guid}/tasks")]
public class ProjectTasksController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TaskResponse>>> GetByProject(
        Guid projectId,
        [FromServices] ITaskService service,
        CancellationToken ct)
        => Ok(await service.GetByProjectAsync(projectId, ct));

    [HttpPost]
    public async Task<ActionResult<TaskResponse>> Create(
        Guid projectId,
        [FromBody] CreateTaskRequest request,
        [FromServices] ITaskService service,
        CancellationToken ct)
    {
        var created = await service.CreateAsync(projectId, request, ct);
        return Created($"/api/tasks/{created.Id}", created);
    }
}
