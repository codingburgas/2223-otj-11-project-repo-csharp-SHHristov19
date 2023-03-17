using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

[Keyless]
public partial class Grade
{
    [StringLength(50)]
    [Unicode(false)]
    public string StudentId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ExamId { get; set; } = null!;

    [Column("Grade")]
    public int? Grade1 { get; set; }

    [ForeignKey("ExamId")]
    public virtual Exam Exam { get; set; } = null!;

    [ForeignKey("StudentId")]
    public virtual Student Student { get; set; } = null!;
}
