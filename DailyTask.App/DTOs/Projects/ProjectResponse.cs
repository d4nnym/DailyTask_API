/*using System;
using System.Collections.Generic;
using System.Text;
*/

namespace DailyTask.App.DTOs.Projects;

public sealed record ProjectResponse(Guid Id, string Name, string? Description, DateTime CreatedAtUtc);
