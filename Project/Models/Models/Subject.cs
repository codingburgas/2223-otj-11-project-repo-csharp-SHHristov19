using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Subject
{
    public string Id { get; set; } = null!;

    public string TeacherId { get; set; } = null!;

    public string SpecialityId { get; set; } = null!;

    public string Name { get; set; }

    public string Type { get; set; }

    public decimal Credits { get; set; }

    public int List { get; set; }

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual Staff Teacher { get; set; } = null!;
}
