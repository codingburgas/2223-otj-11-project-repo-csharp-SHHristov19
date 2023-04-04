using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Univers.Models.Models;

public class FacultyModel
{
    public string? Id { get; set; } = null!;

    public string? UniversityId { get; set; } = null!;

    public string? DeanId { get; set; } = null!;

    public string? ViceDeanId { get; set; } = null!;

    public string? Name { get; set; } = null!;

    public string? Code { get; set; } = null!; 

    [Required(ErrorMessage = "Необходимо е да изберете специалност")]
    public string? Choise { get; set; }
}
