using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

[PrimaryKey("StudentId", "ExamId")]
public partial class Grade
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string StudentId { get; set; } = null!;

    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string ExamId { get; set; } = null!;

    [Column("Grade")]
    public int? Grade1 { get; set; }

    [ForeignKey("ExamId")]
    [InverseProperty("Grades")]
    public virtual Exam Exam { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("Grades")]
    public virtual Student Student { get; set; } = null!;
}
