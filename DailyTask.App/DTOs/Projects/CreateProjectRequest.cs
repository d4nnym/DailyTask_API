/*using System;
using System.Collections.Generic;
using System.Text;*/

namespace DailyTask.App.DTOs.Projects;

public sealed record CreateProjectRequest(string Name, string? Description);