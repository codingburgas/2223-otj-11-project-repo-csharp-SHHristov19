﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

public partial class Faculty
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? UniversityId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? DeanId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ViceDeanId { get; set; }

    [StringLength(200)]
    public string? Name { get; set; }

    [StringLength(20)]
    public string? Code { get; set; }

    [ForeignKey("DeanId")]
    [InverseProperty("FacultyDeans")]
    public virtual Staff? Dean { get; set; }

    [ForeignKey("UniversityId")]
    [InverseProperty("Faculties")]
    public virtual University? University { get; set; }

    [ForeignKey("ViceDeanId")]
    [InverseProperty("FacultyViceDeans")]
    public virtual Staff? ViceDean { get; set; }

    [ForeignKey("FacultyId")]
    [InverseProperty("Faculties")]
    public virtual ICollection<Speciality> Specialities { get; set; } = new List<Speciality>();
}
