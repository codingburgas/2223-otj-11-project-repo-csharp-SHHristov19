using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class GradeModel
{
    public string? StudentId { get; set; } = null!;

    public string? Subject { get; set; } = null!;

    public int? Semester { get; set; } = null!;

    public int? Grade { get; set; } = null!;
} 