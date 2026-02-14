using DailyTask.App.DTOs.Tasks;

namespace DailyTask.App.Interfaces;

public interface ITaskService
{
    Task<TaskResponse> CreateAsync(Guid projectId, CreateTaskRequest request, CancellationToken ct);
    Task<IReadOnlyList<TaskResponse>> GetByProjectAsync(Guid projectId, CancellationToken ct);
    Task<bool> MarkDoneAsync(Guid taskId, CancellationToken ct);
    Task<bool> DeleteAsync(Guid taskId, CancellationToken ct);
}
