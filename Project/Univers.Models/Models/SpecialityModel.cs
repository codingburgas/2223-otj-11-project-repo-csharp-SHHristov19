using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class SpecialityModel
{
    public string? Id { get; set; } = null!;

    public string? TutorId { get; set; } = null!;

    public string? Name { get; set; } = null!;

    public string? Degree { get; set; } = null!;

    public int? Semesters { get; set; } = null!;

    public string? Code { get; set; } = null!;

    public StaffModel? Tutor { get; set; } = null!;
}
