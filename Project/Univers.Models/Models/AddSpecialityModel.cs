using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Univers.Models.Models
{
    public class AddSpecialityModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете тютор на специалността.")]
        [Display(Name = "Декан")]
        public string? TutorId { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете име на специалността.")]
        [Display(Name = "Име")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете степен на образование на специалността.")] 
        public string? Degree { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете брой семестри в специалността.")] 
        [RegularExpression("^(1?[0-9]|20)$", ErrorMessage = "Число между 1 и 20!")]
        [Display(Name = "Семестри")]
        public int? Semesters { get; set; }

        [Required(ErrorMessage = "Необходимо е да въведете код на специалността.")]
        [RegularExpression("^([A-ZА-Я]+)$", ErrorMessage = "Кодът трябва да е само от главни букви!")]
        [Display(Name = "Код")]
        public string? Code { get; set; }

        public UserModel? Tutor { get; set; }

        public string? FacultyId { get; set; }
    }
}
