using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class SubjectComponent
{
    public string InstructorId { get; set; } = null!;

    public string ComponentId { get; set; } = null!;

    public string SubjectId { get; set; } = null!;

    public virtual Component Component { get; set; } = null!;

    public virtual Staff Instructor { get; set; } = null!;

    public virtual Subject Subject { get; set; } = null!;
}
