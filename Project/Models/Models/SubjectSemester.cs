using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class SubjectSemester
{
    public string SemesterId { get; set; } = null!;

    public string SubjectId { get; set; } = null!;

    public virtual Semester Semester { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
