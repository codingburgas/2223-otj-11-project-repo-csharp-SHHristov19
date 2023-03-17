using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

[Keyless]
[Table("StudentCourse")]
public partial class StudentCourse
{
    [StringLength(50)]
    [Unicode(false)]
    public string StudentId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string SemesterId { get; set; } = null!;

    public int? Course { get; set; }

    [ForeignKey("SemesterId")]
    public virtual Semester Semester { get; set; } = null!;

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; } = null!;
}
