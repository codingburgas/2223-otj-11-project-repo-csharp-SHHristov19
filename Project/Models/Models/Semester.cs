using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Semester
{
    public string Id { get; set; } = null!;

    public string UniversityId { get; set; } = null!;

    public int Number { get; set; }

    public string Type { get; set; }

    public DateTime DateOfStart { get; set; }

    public DateTime DateOfEnd { get; set; }

    public virtual University University { get; set; } = null!;
}
