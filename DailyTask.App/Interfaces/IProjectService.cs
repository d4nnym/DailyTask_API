using DailyTask.App.DTOs.Projects;

namespace DailyTask.App.Interfaces;

public interface IProjectService
{
    Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct);
    Task<IReadOnlyList<ProjectResponse>> GetAllAsync(CancellationToken ct);
    Task<ProjectResponse?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<bool> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken ct);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct);
}
