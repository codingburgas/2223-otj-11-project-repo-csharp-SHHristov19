using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.Models.Models
{
    public class AddFacultyModel
    {
        public string? Id { get; set; } = null!;

        public string? UniversityId { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете декан на факултета.")]
        [Display(Name = "Декан")]
        public string? DeanId { get; set; } = null!;

        [Display(Name = "Заместник-Декан")]
        public string? ViceDeanId { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете име на факултета.")]
        [Display(Name = "Име")]
        public string? Name { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете код на факултета.")]
        [RegularExpression("^([A-ZА-Я]+)$", ErrorMessage = "Кодът трябва да е само от главни букви!")]
        [Display(Name = "Код")]
        public string? Code { get; set; } = null!;
         
        public StaffModel? Dean { get; set; } = null!;
         
        public StaffModel? ViceDean { get; set; } = null!;
    }
}
