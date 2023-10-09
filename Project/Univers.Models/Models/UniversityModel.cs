using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Univers.Models.Models;

public class UniversityModel
{
    public string? Id { get; set; } = null!;

    public string? RectorId { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете име на университета.")]
    [Display(Name = "Име")]
    public string? Name { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете адрес на университета.")]
    [Display(Name = "Адрес")]
    public string? Address { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете максимален брой студенти възможни в университета.")]
    [RegularExpression("^(?:[1-9]\\d{2,4}|100000)$", ErrorMessage = "Число между 100 и 100 000!")]
    [Display(Name = "Капацитет")]
    public int? Capacity { get; set; } = null!;
      
    [Display(Name = "Ректор")]
    public StaffModel? Rector { get; set; } = null!;

    public string? RectorName { get; set; } = null!;
}
