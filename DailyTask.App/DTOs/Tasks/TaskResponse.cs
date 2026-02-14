namespace DailyTask.App.DTOs.Tasks;

public sealed record TaskResponse(
    Guid Id,
    Guid ProjectId,
    string Title,
    string? Notes,
    bool IsDone,
    DateTime CreatedAtUtc,
    DateTime? DueAtUtc
);
