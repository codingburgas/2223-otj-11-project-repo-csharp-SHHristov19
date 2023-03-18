using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class Subject
{
    public string? Id { get; set; } = null!;

    public string? TeacherId { get; set; } = null!;

    public string? SpecialityId { get; set; } = null!;

    public string? Name { get; set; } = null!;

    public string? Type { get; set; } = null!;

    public decimal Credits { get; set; } = 0m;

    public int? List { get; set; } = null!;

    public Speciality? Speciality { get; set; } = null!;

    public Staff? Teacher { get; set; } = null!;
}
