using DailyTask.App.DTOs.Projects;
using DailyTask.App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DailyTask.Api.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProjectResponse>>> GetAll(
        [FromServices] IProjectService service,
        CancellationToken ct)
        => Ok(await service.GetAllAsync(ct));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectResponse>> GetById(
        Guid id,
        [FromServices] IProjectService service,
        CancellationToken ct)
    {
        var project = await service.GetByIdAsync(id, ct);
        return project is null ? NotFound() : Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectResponse>> Create(
        [FromBody] CreateProjectRequest request,
        [FromServices] IProjectService service,
        CancellationToken ct)
    {
        var created = await service.CreateAsync(request, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateProjectRequest request,
        [FromServices] IProjectService service,
        CancellationToken ct)
        => await service.UpdateAsync(id, request, ct) ? NoContent() : NotFound();

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        [FromServices] IProjectService service,
        CancellationToken ct)
        => await service.DeleteAsync(id, ct) ? NoContent() : NotFound();
}
