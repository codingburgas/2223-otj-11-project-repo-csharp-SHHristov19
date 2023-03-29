using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class GradeModel
{
    public string? StudentId { get; set; } = null!;

    public string? ExamId { get; set; } = null!;

    public int? Grades { get; set; } = null!;

    public ExamModel? Exam { get; set; } = null!;

    public StudentModel? Student { get; set; } = null!;
}
