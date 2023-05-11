using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

public partial class Subject
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? TeacherId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SpecialityId { get; set; }

    [StringLength(150)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? Type { get; set; }

    [Column(TypeName = "decimal(2, 1)")]
    public decimal? Credits { get; set; }

    public int? List { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    [ForeignKey("SpecialityId")]
    [InverseProperty("Subjects")]
    public virtual Speciality? Speciality { get; set; }

    [InverseProperty("Subject")]
    public virtual ICollection<SubjectComponent> SubjectComponents { get; set; } = new List<SubjectComponent>();

    [ForeignKey("TeacherId")]
    [InverseProperty("Subjects")]
    public virtual Staff? Teacher { get; set; }

    [ForeignKey("SubjectId")]
    [InverseProperty("Subjects")]
    public virtual ICollection<Semester> Semesters { get; set; } = new List<Semester>();
}
