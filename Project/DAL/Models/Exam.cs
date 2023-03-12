using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class Exam
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ProctorId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string SubjectId { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ExamSessionId { get; set; } = null!;

    public DateTime? TimeOfStart { get; set; }

    public DateTime? TimeOfEnd { get; set; }

    [StringLength(50)]
    public string? Location { get; set; }

    [ForeignKey("ExamSessionId")]
    [InverseProperty("Exams")]
    public virtual ExamSession ExamSession { get; set; } = null!;

    [ForeignKey("ProctorId")]
    [InverseProperty("Exams")]
    public virtual Staff Proctor { get; set; } = null!;

    [ForeignKey("SubjectId")]
    [InverseProperty("Exams")]
    public virtual Subject Subject { get; set; } = null!;
}
