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

        public List<UserModel>? Deans { get; set; }

        public List<UserModel>? ViceDeans { get; set; }

        public List<UserModel>? Tutors { get; set; } 

        public UserModel? ChosenUser { get; set; }

        public StudentModel? ChosenStudent { get; set; }

        public UniversityModel? ChosenUniversity { get; set; }

        public FacultyModel? ChosenFaculty { get; set; }

        public SpecialityModel? ChosenSpeciality { get; set; }

        public EditUserModel? EditUser { get; set; }

        public AddUserModel? AddUser { get; set; }

        public EditStudentModel? EditStudent { get; set; }

        public AddStudentModel? AddStudent { get; set; }

        public UniversityModel? AddUniversity { get; set; }

        public AddFacultyModel? AddFaculty { get; set; }

        public AddSpecialityModel? AddSpeciality { get; set; }

        public SemesterModel? AddSemester { get; set; }

        public IEnumerable<UniversityModel>? Universities { get; set;}

        public IEnumerable<FacultyModel>? Faculties { get; set;}

        public List<SpecialityModel>? Specialities { get; set;} 

        public IEnumerable<SpecialityModel>? SpecialitiesBachelor { get; set;} 

        public IEnumerable<SpecialityModel>? SpecialitiesMagister { get; set;} 

        public IEnumerable<SemesterModel>? Semesters { get; set;} 

        public IEnumerable<SubjectModel>? Subjects { get; set;} 
    }
}
