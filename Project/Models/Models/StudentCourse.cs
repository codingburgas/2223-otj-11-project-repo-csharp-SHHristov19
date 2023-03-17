using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class StudentCourse
{
    public string StudentId { get; set; } = null!;

    public string SemesterId { get; set; } = null!;

    public int Course { get; set; }

    public virtual Semester Semester { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
