using DailyTask.App.DTOs.Tasks;
using DailyTask.App.Interfaces;
using DailyTask.Domain.Entities;
using DailyTask.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DailyTask.App.Services;

/*public sealed class TaskService : ITaskService
{
    private readonly DailyTaskDbContext _db;

    public TaskService(DailyTaskDbContext db) => _db = db;*/
public sealed class TaskService(DailyTaskDbContext db) : ITaskService
{

    public async Task<TaskResponse> CreateAsync(Guid projectId, CreateTaskRequest request, CancellationToken ct) {
        var projectExists = await db.Projects.AsNoTracking().AnyAsync(p => p.Id == projectId, ct);
        if (!projectExists)
            throw new InvalidOperationException($"Project '{projectId}' does not exist.");

        var entity = new TaskItem
        {
            ProjectId = projectId,
            Title = request.Title.Trim(),
            Notes = request.Notes?.Trim(),
            DueAtUtc = request.DueAtUtc
        };

        db.Tasks.Add(entity);
        await db.SaveChangesAsync(ct);

        return new TaskResponse(
            entity.Id, entity.ProjectId, entity.Title, entity.Notes,
            entity.IsDone, entity.CreatedAtUtc, entity.DueAtUtc
        );
    }

    public async Task<IReadOnlyList<TaskResponse>> GetByProjectAsync(Guid projectId, CancellationToken ct)
    {
        return await db.Tasks
            .AsNoTracking()
            .Where(t => t.ProjectId == projectId)
            .OrderBy(t => t.IsDone)
            .ThenByDescending(t => t.CreatedAtUtc)
            .Select(t => new TaskResponse(
                t.Id, t.ProjectId, t.Title, t.Notes,
                t.IsDone, t.CreatedAtUtc, t.DueAtUtc))
            .ToListAsync(ct);
    }

    public async Task<bool> MarkDoneAsync(Guid taskId, CancellationToken ct)
    {
        var entity = await db.Tasks.FirstOrDefaultAsync(t => t.Id == taskId, ct);
        if (entity is null) return false;

        if (!entity.IsDone)
        {
            entity.IsDone = true;
            await db.SaveChangesAsync(ct);
        }

        return true;
    }

    public async Task<bool> DeleteAsync(Guid taskId, CancellationToken ct)
    {
        var entity = await db.Tasks.FirstOrDefaultAsync(t => t.Id == taskId, ct);
        if (entity is null) return false;

        db.Tasks.Remove(entity);
        await db.SaveChangesAsync(ct);
        return true;
    }
}
