using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class University
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string RectorId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Address { get; set; }

    public int? Capacity { get; set; }

    [InverseProperty("University")]
    public virtual ICollection<Faculty> Faculties { get; } = new List<Faculty>();

    [ForeignKey("RectorId")]
    [InverseProperty("Universities")]
    public virtual Staff Rector { get; set; } = null!;

    [InverseProperty("University")]
    public virtual ICollection<Semester> Semesters { get; } = new List<Semester>();
}
