using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class StudentCourseModel
{
    public string? StudentId { get; set; } = null!;

    public string? SemesterId { get; set; } = null!;

    public int? Course { get; set; } = null!;

    public SemesterModel? Semester { get; set; } = null!;

    public StudentModel? Student { get; set; } = null!;
}
