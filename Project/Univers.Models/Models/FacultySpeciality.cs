﻿using System;
using System.Collections.Generic;

namespace UniversModels.Models;

public class FacultySpeciality
{
    public string? FacultyId { get; set; } = null!;

    public string? SpecialityId { get; set; } = null!;

    public Faculty? Faculty { get; set; } = null!;

    public Speciality? Speciality { get; set; } = null!;
}