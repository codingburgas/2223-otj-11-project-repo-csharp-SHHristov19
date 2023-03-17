using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Grade
{
    public string StudentId { get; set; } = null!;

    public string ExamId { get; set; } = null!;

    public int Grade1 { get; set; }

    public virtual Exam Exam { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
