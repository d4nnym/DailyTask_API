namespace DailyTask.App.DTOs.Tasks;

public sealed record CreateTaskRequest(string Title, string? Notes, DateTime? DueAtUtc);
