using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Univers.Models.Models;

public class StudentModel : SignUpUserModel
{
    public string? Id { get; set; } = null!;

    public string? SpecialityId { get; set; } = null!;

    public string? UserId { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете ЕГН.")]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Невалидно ЕГН.")]
    public string? Identity { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете гражданство.")]
    public string? Citizenship { get; set; } = null!; 

    public DateTime? DateOfStarting { get; set; } = null!;

    public DateTime? DateOfGraduate { get; set; } = null!;

    public string? FormOfEducation { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете дата на раждате.")]
    public DateTime? DateOfBirth { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете страна на раждане.")]
    public string? CountryOfBirth { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете община на раждане.")]
    public string? MunicipalityOfBirth { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете област на раждане.")]
    public string? AreaOfBirth { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете град на раждане.")]
    public string? CityOfBirth { get; set; } = null!;

    public string? FacultyNumber { get; set; } = null!;

    public string? Degree { get; set; } = null!;

    public string? NameOfUniversity = null!;

    public string? StudentCourse = null!;
}
