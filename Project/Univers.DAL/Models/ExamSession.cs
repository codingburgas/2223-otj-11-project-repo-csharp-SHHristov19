using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class ExamSession
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string SemesterId { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime? DateOfStart { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DateOfEnd { get; set; }

    [StringLength(100)]
    public string? Type { get; set; }

    [InverseProperty("ExamSession")]
    public virtual ICollection<Exam> Exams { get; } = new List<Exam>();

    [ForeignKey("SemesterId")]
    [InverseProperty("ExamSessions")]
    public virtual Semester Semester { get; set; } = null!;
}
