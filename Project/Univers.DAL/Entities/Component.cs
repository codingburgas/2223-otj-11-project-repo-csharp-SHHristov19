using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

public partial class Component
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(60)]
    public string? Type { get; set; }

    public string? Activity { get; set; }

    public DateTime? DateOfStart { get; set; }

    public TimeSpan? Duration { get; set; }

    [StringLength(50)]
    public string? Location { get; set; }

    public string? Note { get; set; }

    [InverseProperty("Component")]
    public virtual ICollection<SubjectComponent> SubjectComponents { get; set; } = new List<SubjectComponent>();
}
