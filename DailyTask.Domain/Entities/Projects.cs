/*using System;
using System.Collections.Generic;
using System.Text;*/

namespace DailyTask.Domain.Entities;

public class Project{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}