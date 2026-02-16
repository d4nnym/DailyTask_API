using DailyTask.App.DTOs.Projects;
using DailyTask.App.Interfaces;
using DailyTask.Domain.Entities;
using DailyTask.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DailyTask.Infrastructure.Services;
/*
public sealed class ProjectService : IProjectService
{
    private readonly DailyTaskDbContext _db;

    public ProjectService(DailyTaskDbContext db) => _db = db;*/
public sealed class ProjectService(DailyTaskDbContext db) : IProjectService{

    public async Task<ProjectResponse> CreateAsync(CreateProjectRequest request, CancellationToken ct)
    {
        var entity = new Project
        {
            Name = request.Name.Trim(),
            Description = request.Description?.Trim()
        };

        db.Projects.Add(entity);
        await db.SaveChangesAsync(ct);

        return new ProjectResponse(entity.Id, entity.Name, entity.Description, entity.CreatedAtUtc);
    }

    public async Task<IReadOnlyList<ProjectResponse>> GetAllAsync(CancellationToken ct)
    {
        return await db.Projects
            .AsNoTracking()
            .OrderByDescending(p => p.CreatedAtUtc)
            .Select(p => new ProjectResponse(p.Id, p.Name, p.Description, p.CreatedAtUtc))
            .ToListAsync(ct);
    }

    public async Task<ProjectResponse?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        return await db.Projects
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProjectResponse(p.Id, p.Name, p.Description, p.CreatedAtUtc))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken ct)
    {
        var entity = await db.Projects.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (entity is null) return false;

        entity.Name = request.Name.Trim();
        entity.Description = request.Description?.Trim();

        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await db.Projects.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (entity is null) return false;

        db.Projects.Remove(entity);
        await db.SaveChangesAsync(ct);
        return true;
    }
}
