using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Univers.DAL.Entities;

[Keyless]
public partial class Holiday
{
    [StringLength(100)]
    public string? Name { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DateOfStart { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DateOfEnd { get; set; }
}
