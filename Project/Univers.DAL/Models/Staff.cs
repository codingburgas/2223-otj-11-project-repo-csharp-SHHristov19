using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class Staff
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string UserId { get; set; } = null!;

    [StringLength(50)]
    public string Role { get; set; } = null!;

    [InverseProperty("Proctor")]
    public virtual ICollection<Exam> Exams { get; } = new List<Exam>();

    [InverseProperty("Dean")]
    public virtual ICollection<Faculty> FacultyDeans { get; } = new List<Faculty>();

    [InverseProperty("ViceDean")]
    public virtual ICollection<Faculty> FacultyViceDeans { get; } = new List<Faculty>();

    [InverseProperty("Tutor")]
    public virtual ICollection<Speciality> Specialities { get; } = new List<Speciality>();

    [InverseProperty("Teacher")]
    public virtual ICollection<Subject> Subjects { get; } = new List<Subject>();

    [InverseProperty("Rector")]
    public virtual ICollection<University> Universities { get; } = new List<University>();

    [ForeignKey("UserId")]
    [InverseProperty("Staff")]
    public virtual User User { get; set; } = null!;
}
