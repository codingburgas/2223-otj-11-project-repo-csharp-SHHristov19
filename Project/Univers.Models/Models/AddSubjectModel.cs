using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Univers.Models.Models
{
    public class AddSubjectModel
    {
        public string? Id { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете преподавател на предмета.")]
        [Display(Name = "Преподавател")]
        public string? TeacherId { get; set; } = null!;

        public string? SpecialityId { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете име на предмета.")]
        [Display(Name = "Име")]
        public string? Name { get; set; } = null!;

        public string? Type { get; set; } = null!;

        [Required(ErrorMessage = "Необходимо е да въведете брой кредити на предмета.")]
        [Display(Name = "Кредити")]
        public decimal? Credits { get; set; } = 0m;

        public int? List { get; set; } = null!;

        public string? Speciality { get; set; } = null!;

        public StaffModel? Teacher { get; set; } = null!;

        public int? Number { get; set; } = null!;

        public string? TeacherName { get; set; } = null!;
    }
}
