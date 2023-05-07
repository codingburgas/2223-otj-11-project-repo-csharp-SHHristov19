using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

public partial class Semester
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? UniversityId { get; set; }

    public int? Number { get; set; }

    [StringLength(50)]
    public string? Type { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DateOfStart { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DateOfEnd { get; set; }

    [InverseProperty("Semester")]
    public virtual ICollection<ExamSession> ExamSessions { get; set; } = new List<ExamSession>();

    [ForeignKey("UniversityId")]
    [InverseProperty("Semesters")]
    public virtual University? University { get; set; }
}
