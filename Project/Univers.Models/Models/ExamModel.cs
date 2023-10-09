using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class ExamModel
{
    public string? Id { get; set; } = null!;

    public string? ProctorId { get; set; } = null!;

    public string? SubjectId { get; set; } = null!;

    public string? ExamSessionId { get; set; } = null!;

    public DateTime? TimeOfStart { get; set; } = null!;

    public DateTime? TimeOfEnd { get; set; } = null!;

    public string? Location { get; set; } = null!; 

    public string? Proctor { get; set; } = null!;

    public string? Subject { get; set; } = null!;

    public string? Type { get; set; } = null!;
}
