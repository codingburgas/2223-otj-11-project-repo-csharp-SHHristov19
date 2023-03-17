using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

[Keyless]
public partial class SubjectComponent
{
    [StringLength(50)]
    [Unicode(false)]
    public string InstructorId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ComponentId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string SubjectId { get; set; } = null!;

    [ForeignKey("ComponentId")]
    public virtual Component Component { get; set; } = null!;

    [ForeignKey("InstructorId")]
    public virtual Staff Instructor { get; set; } = null!;

    [ForeignKey("SubjectId")]
    public virtual Subject Subject { get; set; } = null!;
}
