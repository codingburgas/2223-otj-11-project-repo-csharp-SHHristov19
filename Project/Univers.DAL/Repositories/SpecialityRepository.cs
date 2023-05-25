using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Context;
using Univers.DAL.Entities;
using Univers.Models.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Univers.DAL.Repositories
{
    public class SpecialityRepository
    {
        /// <summary>
        /// Read the data from the Speciality table
        /// </summary>
        /// <returns></returns>
        public List<Speciality> ReadAllData()
        {
            using Context.Context context = new();

            return context.Specialities.ToList();
        }

        public string? GetSpecialityNameByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from speciality in context.Specialities
                    join student in context.Students on speciality.Id equals student.SpecialityId
                    where student.Id == studentId
                    select speciality.Name).FirstOrDefault();
        }

        public string? GetTutorNameByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from speciality in context.Specialities
                    join student in context.Students on speciality.Id equals student.SpecialityId
                    join staff in context.Staff on speciality.TutorId equals staff.Id
                    join user in context.Users on staff.UserId equals user.Id
                    where student.Id == studentId
                    select new
                    {
                        Name = $"{user.FirstName} {user.MiddleName} {user.LastName}"
                    })?.FirstOrDefault()?.Name;
        }

        public string? GetDegreeByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from speciality in context.Specialities
                    join student in context.Students on speciality.Id equals student.SpecialityId
                    where student.Id == studentId
                    select speciality.Degree).FirstOrDefault();
        }

        public List<Speciality> GetSpecialitiesByFacultyId(string facultyId, string degree)
        {
            using Context.Context context = new();

            return (from speciality in context.Specialities
                    join f in context.Faculties on speciality.Faculties.First().Id equals f.Id
                    where f.Id == facultyId && speciality.Degree == degree
                    select speciality).ToList();

        }

        public void AddSpeciality(AddSpecialityModel? addSpeciality)
        {
            using Context.Context context = new();

            var faculty = context.Faculties.FirstOrDefault(x => x.Id == addSpeciality.FacultyId);

            var speciality = new Speciality()
            {
                Id = Guid.NewGuid().ToString("D"),
                Name = addSpeciality.Name,
                Code = addSpeciality.Code,
                TutorId = addSpeciality.TutorId,
                Degree = addSpeciality.Degree,
                Semesters = addSpeciality.Semesters,
            };

            faculty.Specialities.Add(speciality);
            context.Specialities.Add(speciality);
            context.SaveChanges();
        }

        public void DeleteSpeciality(string specialityId)
        {
            using Context.Context context = new();

            var speciality = context.Specialities
                            .Include(s => s.Faculties)
                            .FirstOrDefault(s => s.Id == specialityId);

            speciality.Faculties.Clear();

            context.Specialities.Remove(speciality);
            context.SaveChanges();
        }
    }
}