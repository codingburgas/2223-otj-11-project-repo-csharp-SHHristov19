using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Exam
{
    public string Id { get; set; } = null!;

    public string ProctorId { get; set; } = null!;

    public string SubjectId { get; set; } = null!;

    public string ExamSessionId { get; set; } = null!;

    public DateTime TimeOfStart { get; set; }

    public DateTime TimeOfEnd { get; set; }

    public string Location { get; set; }

    public virtual ExamSession ExamSession { get; set; } = null!;

    public virtual Staff Proctor { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
