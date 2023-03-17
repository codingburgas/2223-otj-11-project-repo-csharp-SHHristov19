using System;
using System.Collections.Generic;

namespace UniversModels.Models;

public class SubjectComponent
{
    public string? InstructorId { get; set; } = null!;

    public string? ComponentId { get; set; } = null!;

    public string? SubjectId { get; set; } = null!;

    public Component? Component { get; set; } = null!;

    public Staff? Instructor { get; set; } = null!;

    public Subject? Subject { get; set; } = null!;
}
