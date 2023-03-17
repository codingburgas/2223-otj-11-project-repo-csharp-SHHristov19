using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Student
{
    public string Id { get; set; } = null!;

    public string SpecialityId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Identity { get; set; }

    public string Citizenship { get; set; }

    public string DacultyNumber { get; set; }

    public DateTime DateOfStarting { get; set; }

    public DateTime DateOfGraduate { get; set; }

    public string FormOfEducation { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string CountryOfBirth { get; set; }

    public string MunicipalityOfBirth { get; set; }

    public string AreaOfBirth { get; set; }

    public string CityOfBirth { get; set; }

    public string FacultyNumber { get; set; }

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
