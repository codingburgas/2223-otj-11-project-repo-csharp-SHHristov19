﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

public partial class Speciality
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? TutorId { get; set; }

    [StringLength(200)]
    public string? Name { get; set; }

    [StringLength(100)]
    public string? Degree { get; set; }

    public int? Semesters { get; set; }

    [StringLength(20)]
    public string? Code { get; set; }

    [InverseProperty("Speciality")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    [InverseProperty("Speciality")]
    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();

    [ForeignKey("TutorId")]
    [InverseProperty("Specialities")]
    public virtual Staff? Tutor { get; set; }

    [ForeignKey("SpecialityId")]
    [InverseProperty("Specialities")]
    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();
}
