using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class FacultySpeciality
{
    public string? FacultyId { get; set; } = null!;

    public string? SpecialityId { get; set; } = null!;

    public FacultyModel? Faculty { get; set; } = null!;

    public SpecialityModel? Speciality { get; set; } = null!;
}
