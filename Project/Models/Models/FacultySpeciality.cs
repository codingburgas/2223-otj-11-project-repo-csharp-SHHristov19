using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class FacultySpeciality
{
    public string FacultyId { get; set; } = null!;

    public string SpecialityId { get; set; } = null!;

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual Speciality Speciality { get; set; } = null!;
}
