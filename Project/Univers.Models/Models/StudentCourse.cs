using System;
using System.Collections.Generic;

namespace UniversModels.Models;

public class StudentCourse
{
    public string? StudentId { get; set; } = null!;

    public string? SemesterId { get; set; } = null!;

    public int? Course { get; set; } = null!;

    public Semester? Semester { get; set; } = null!;

    public Student? Student { get; set; } = null!;
}
