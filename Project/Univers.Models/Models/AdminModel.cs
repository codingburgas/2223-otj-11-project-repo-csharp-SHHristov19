using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.Models.Models
{
    public class AdminModel
    {
        public string? UserId { get; set; }

        public List<UserModel>? Users { get; set; }

        public UserModel? ChosenUser { get; set; }

        public StudentModel? ChosenStudent { get; set; }

        public UniversityModel? ChosenUniversity { get; set; }

        public FacultyModel? ChosenFaculty { get; set; }

        public EditUserModel? EditUser { get; set; }

        public AddUserModel? AddUser { get; set; }

        public EditStudentModel? EditStudent { get; set; }

        public AddStudentModel? AddStudent { get; set; }

        public IEnumerable<UniversityModel>? Universities { get; set;}

        public IEnumerable<FacultyModel>? Faculties { get; set;}

        public List<SpecialityModel>? Specialities { get; set;} 

        public IEnumerable<SpecialityModel>? SpecialitiesBachelor { get; set;} 

        public IEnumerable<SpecialityModel>? SpecialitiesMagister { get; set;} 
    }
}
