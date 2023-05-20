using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Univers.Models.Models
{
    public class AddStudentModel
    {
        public string? Id { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете ЕГН.")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Невалидно ЕГН.")]
        [Display(Name = "ЕГН")]
        public string? Identity { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете гражданство.")]
        [Display(Name = "Гражданство")]
        public string? Citizenship { get; set; } = null!;

        [Display(Name = "Дата на стартиране")]
        public string? DateOfStarting { get; set; } = null!;

        [Display(Name = "Дата на завършване")]
        public string? DateOfGraduate { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете форма на обучение.")]
        [Display(Name = "Форма на обучение")]
        public string? FormOfEducation { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете дата на раждате.")]
        [Display(Name = "Дата на раждане")]
        public string? DateOfBirth { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете страна на раждане.")]
        [Display(Name = "Държава на раждане")]
        public string? CountryOfBirth { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете община на раждане.")]
        [Display(Name = "Община на раждане")]
        public string? MunicipalityOfBirth { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете област на раждане.")]
        [Display(Name = "Област на раждане")]
        public string? AreaOfBirth { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете град на раждане.")]
        [Display(Name = "Град на раждане")]
        public string? CityOfBirth { get; set; } = null!;

        [Display(Name = "Факултетен номер")]
        public string? FacultyNumber { get; set; } = null!;

        [Display(Name = "Образователно-квалификационна степен")]
        public string? Degree { get; set; } = null!;

        [Display(Name = "Университет")]
        public string? NameOfUniversity { get; set; } = null!;

        public AddUserModel? User { get; set; } = null!;
    }
}