using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class Component
{
    [Key]
    [StringLength(50)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(60)]
    public string Type { get; set; } = null!;

    public string? Activity { get; set; }

    public DateTime? DateOfStart { get; set; }

    public TimeSpan? Duration { get; set; }

    [StringLength(50)]
    public string? Location { get; set; }

    public string? Note { get; set; }
}
