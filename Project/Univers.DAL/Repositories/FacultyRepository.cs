using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class FacultyRepository
    {
        /// <summary>
        /// Read the data from the Faculty table
        /// </summary>
        /// <returns></returns>
        public List<Faculty> ReadAllData()
        {
            using Context.Context context = new();

            return context.Faculties.ToList();
        }

        public string? GetDeanNameByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from speciality in context.Specialities
                    join student in context.Students on speciality.Id equals student.SpecialityId
                    join facultySeciality in context.FacultySpecialities on speciality.Id equals facultySeciality.SpecialityId
                    join faculty in context.Faculties on facultySeciality.FacultyId equals faculty.Id
                    join staff in context.Staff on faculty.DeanId equals staff.Id
                    join user in context.Users on staff.UserId equals user.Id
                    where student.Id == studentId
                    select new
                    {
                        Name = $"{user.FirstName} {user.MiddleName} {user.LastName}"
                    }).FirstOrDefault().Name;
        }
    }
}
