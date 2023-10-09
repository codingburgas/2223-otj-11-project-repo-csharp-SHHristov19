using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;
using Univers.Models.Models;

namespace Univers.DAL.Repositories
{
    public class FacultyRepository
    {
        private readonly SpecialityRepository _specialityRepository;

        public FacultyRepository()
        {
            _specialityRepository = new SpecialityRepository();
        }
        /// <summary>
        /// Read the data from the Faculty table
        /// </summary>
        /// <returns></returns>
        public List<Faculty> ReadAllData()
        {
            using Context.Context context = new();

            return context.Faculties.ToList();
        }

        /// <summary>
        /// Retrieves the name of the dean associated with a given student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The name of the dean associated with the student, or null if not found.</returns>
        public string? GetDeanNameByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from sp in context.Specialities
                     join s in context.Students on sp.Id equals s.SpecialityId
                     join f in context.Faculties on sp.Faculties.First().Id equals f.Id
                     join sf in context.Staff on f.DeanId equals sf.Id
                     join u in context.Users on sf.UserId equals u.Id
                     where s.Id == studentId
                    select new
                     {
                         Name = $"{u.FirstName} {u.MiddleName} {u.LastName}"
                     }).First().Name;
        }

        /// <summary>
        /// Adds a new faculty using the provided AddFacultyModel.
        /// </summary>
        /// <param name="addFaculty">The AddFacultyModel containing the details of the faculty to be added.</param>
        public void AddFaculty(AddFacultyModel addFaculty)
        {
            using Context.Context context = new();

            var faculty = new Faculty()
            { 
                Id = Guid.NewGuid().ToString("D"),
                Name = addFaculty.Name,
                Code = addFaculty.Code,
                DeanId = addFaculty.DeanId,
                ViceDeanId = addFaculty.ViceDeanId,
                UniversityId = addFaculty.UniversityId,
            };

            context.Faculties.Add(faculty);
            context.SaveChanges(); 
        }

        /// <summary>
        /// Deletes a faculty based on the provided faculty ID.
        /// </summary>
        /// <param name="facultyId">The ID of the faculty to be deleted.</param>
        public void DeleteFaculty(string facultyId)
        {
            using Context.Context context = new();

            var faculty = context.Faculties
                            .Include(s => s.Specialities)
                            .FirstOrDefault(s => s.Id == facultyId);

             
            foreach (var speciality in faculty.Specialities.ToList())
            {
                speciality.Faculties.Clear();
            } 
            
            context.Faculties.Remove(faculty);
            context.SaveChanges();
        }
    }
}
