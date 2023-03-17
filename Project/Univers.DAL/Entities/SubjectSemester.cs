using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

[Keyless]
[Table("SubjectSemester")]
public partial class SubjectSemester
{
    [StringLength(50)]
    [Unicode(false)]
    public string SemesterId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string SubjectId { get; set; } = null!;

    [ForeignKey("SemesterId")]
    public virtual Semester Semester { get; set; } = null!;

    [ForeignKey("SubjectId")]
    public virtual Subject Subject { get; set; } = null!;
}
