using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class SubjectComponent
{
    public string? InstructorId { get; set; } = null!;

    public string? ComponentId { get; set; } = null!;

    public string? SubjectId { get; set; } = null!;

    public ComponentModel? Component { get; set; } = null!;

    public StaffModel? Instructor { get; set; } = null!;

    public SubjectModel? Subject { get; set; } = null!;
}
