/*using System;
using System.Collections.Generic;
using System.Text;*/

namespace DailyTask.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = default!;

    public string Title { get; set; } = default!;
    public string? Notes { get; set; }

    public bool IsDone { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? DueAtUtc { get; set; }
}