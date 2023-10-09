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

        /// <summary>
        /// Retrieves the name of the speciality associated with a given student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The name of the speciality associated with the student, or null if not found.</returns>
        public string? GetSpecialityNameByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from speciality in context.Specialities
                    join student in context.Students on speciality.Id equals student.SpecialityId
                    where student.Id == studentId
                    select speciality.Name).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves the name of the tutor associated with a given student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The name of the tutor associated with the student, or null if not found.</returns>
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

        /// <summary>
        /// Retrieves the degree associated with a given student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The degree associated with the student, or null if not found.</returns>
        public string? GetDegreeByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from speciality in context.Specialities
                    join student in context.Students on speciality.Id equals student.SpecialityId
                    where student.Id == studentId
                    select speciality.Degree).FirstOrDefault();
        }

        /// <summary>
        /// Retrieves a list of Speciality objects associated with a given faculty ID and degree.
        /// </summary>
        /// <param name="facultyId">The ID of the faculty.</param>
        /// <param name="degree">The degree of the specialities (e.g., undergraduate, postgraduate).</param>
        /// <returns>A list of Speciality objects associated with the faculty ID and degree.</returns>
        public List<Speciality> GetSpecialitiesByFacultyId(string facultyId, string degree)
        {
            using Context.Context context = new();

            return (from speciality in context.Specialities
                    join f in context.Faculties on speciality.Faculties.First().Id equals f.Id
                    where f.Id == facultyId && speciality.Degree == degree
                    select speciality).ToList();

        }

        /// <summary>
        /// Adds a new speciality using the provided AddSpecialityModel.
        /// </summary>
        /// <param name="addSpeciality">The AddSpecialityModel containing the details of the speciality to be added.</param>
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

        /// <summary>
        /// Deletes a speciality based on the provided speciality ID.
        /// </summary>
        /// <param name="specialityId">The ID of the speciality to be deleted.</param>
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