using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class UniversityRepository
    {
        // Read the data from the Universities table
        public List<University> ReadAllData()
        {
            using Context.Context context = new();

            return context.Universities.ToList();
        }

        public string? GetRectorByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from speciality in context.Specialities
                    join student in context.Students on speciality.Id equals student.SpecialityId
                    join facultySpeciality in context.FacultySpecialities on speciality.Id equals facultySpeciality.SpecialityId
                    join faculty in context.Faculties on facultySpeciality.FacultyId equals faculty.Id
                    join university in context.Universities on faculty.UniversityId equals university.Id
                    join staff in context.Staff on university.RectorId equals staff.Id
                    join user in context.Users on staff.UserId equals user.Id
                    where student.Id == studentId
                    select new
                    {
                        Name = $"{user.FirstName} {user.MiddleName} {user.LastName}"
                    }).FirstOrDefault().Name;
        }

        public string? GetUniversityNameByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from sp in context.Specialities
                    join s in context.Students on sp.Id equals s.SpecialityId
                    join fs in context.FacultySpecialities on sp.Id equals fs.SpecialityId
                    join f in context.Faculties on fs.FacultyId equals f.Id
                    join un in context.Universities on f.UniversityId equals un.Id
                    where s.Id == studentId
                    select un.Name).FirstOrDefault();
        }

        public string? GetUniversityAddressByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from sp in context.Specialities
                   join s in context.Students on sp.Id equals s.SpecialityId
                   join fs in context.FacultySpecialities on sp.Id equals fs.SpecialityId
                   join f in context.Faculties on fs.FacultyId equals f.Id
                   join un in context.Universities on f.UniversityId equals un.Id
                   where s.Id == studentId
                   select un.Address).FirstOrDefault();
        }
    }
}
