using System;
using System.Collections.Generic;

namespace UniversModels.Models;

public class Grade
{
    public string? StudentId { get; set; } = null!;

    public string? ExamId { get; set; } = null!;

    public int? Grades { get; set; } = null!;

    public Exam? Exam { get; set; } = null!;

    public Student? Student { get; set; } = null!;
}
