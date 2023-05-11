using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

public partial class Staff
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? UserId { get; set; }

    [StringLength(50)]
    public string? Role { get; set; }

    [InverseProperty("Proctor")]
    public virtual ICollection<Exam> Exams { get; set; } = new List<Exam>();

    [InverseProperty("Dean")]
    public virtual ICollection<Faculty> FacultyDeans { get; set; } = new List<Faculty>();

    [InverseProperty("ViceDean")]
    public virtual ICollection<Faculty> FacultyViceDeans { get; set; } = new List<Faculty>();

    [InverseProperty("Tutor")]
    public virtual ICollection<Speciality> Specialities { get; set; } = new List<Speciality>();

    [InverseProperty("Instructor")]
    public virtual ICollection<SubjectComponent> SubjectComponents { get; set; } = new List<SubjectComponent>();

    [InverseProperty("Teacher")]
    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    [InverseProperty("Rector")]
    public virtual ICollection<University> Universities { get; set; } = new List<University>();

    [ForeignKey("UserId")]
    [InverseProperty("Staff")]
    public virtual User? User { get; set; }
}
