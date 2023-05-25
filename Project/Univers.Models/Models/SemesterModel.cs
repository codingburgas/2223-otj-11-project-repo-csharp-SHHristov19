using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Univers.Models.Models;

public class SemesterModel
{
    public string? Id { get; set; } = null!;

    public string? UniversityId { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете номер на семестъра.")]
    [RegularExpression("^(1?[0-9]|20)$", ErrorMessage = "Число между 1 и 20!")]
    [Display(Name = "Семестър")]
    public int? Number { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете вид на семестъра.")]
    [Display(Name = "Вид на семестър")]
    public string? Type { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете начална дата на семестъра.")]
    [Display(Name = "Начална дата")]
    public DateTime? DateOfStart { get; set; } = null!;

    [Required(ErrorMessage = "Необходимо е да въведете крайна дата на семестъра.")]
    [Display(Name = "Крайна дата")]
    public DateTime? DateOfEnd { get; set; } = null!;

    public UniversityModel? University { get; set; } = null!;
}
