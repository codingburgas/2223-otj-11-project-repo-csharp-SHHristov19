using System;
using System.Collections.Generic;

namespace Univers.Models.Models;

public class Student : User
{
    public string Id { get; set; }

    public string? SpecialityId { get; set; } = null!;

    public string? UserId { get; set; } = null!;

    public string? Identity { get; set; } = null!;

    public string? Citizenship { get; set; } = null!;

    public string? DacultyNumber { get; set; } = null!;

    public DateTime? DateOfStarting { get; set; } = null!;

    public DateTime? DateOfGraduate { get; set; } = null!;

    public string? FormOfEducation { get; set; } = null!;

    public DateTime? DateOfBirth { get; set; } = null!;

    public string? CountryOfBirth { get; set; } = null!;

    public string? MunicipalityOfBirth { get; set; } = null!;

    public string? AreaOfBirth { get; set; } = null!;

    public string? CityOfBirth { get; set; } = null!;

    public string? FacultyNumber { get; set; } = null!;
}
