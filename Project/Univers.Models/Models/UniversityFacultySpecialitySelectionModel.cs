using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.Models.Models
{
    public class UniversityFacultySpecialitySelectionModel
    {
        public StudentModel? Student { get; set; } = null!;

        public List<UniversityModel>? Universities { get; set; } = null!;

        public List<FacultyModel>? Faculties { get; set; } = null!;

        public List<SpecialityModel>? Specialities { get; set; } = null!;
         
        public string? UniversityId { get; set; } = null!;

        public string? FacultyId { get; set; } = null!;

        public string? SpecialityId { get; set; } = null!;

        [Required]
        public string? Choice { get; set; }
    }
}
