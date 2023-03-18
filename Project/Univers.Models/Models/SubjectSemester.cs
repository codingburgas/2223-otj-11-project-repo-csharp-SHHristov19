using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class SubjectSemester
{
    public string? SemesterId { get; set; } = null!;

    public string? SubjectId { get; set; } = null!;

    public Semester? Semester { get; set; } = null!;

    public Subject? Subject { get; set; } = null!;
}
