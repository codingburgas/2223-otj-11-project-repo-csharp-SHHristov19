using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class Semester
{
    public string? Id { get; set; } = null!;

    public string? UniversityId { get; set; } = null!;

    public int? Number { get; set; } = null!;

    public string? Type { get; set; } = null!;

    public DateTime? DateOfStart { get; set; } = null!;

    public DateTime? DateOfEnd { get; set; } = null!;

    public University? University { get; set; } = null!;
}
