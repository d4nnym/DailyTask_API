/*using System;
using System.Collections.Generic;
using System.Text;*/

namespace DailyTask.App.DTOs.Projects;

public sealed record UpdateProjectRequest(string Name, string? Description);
