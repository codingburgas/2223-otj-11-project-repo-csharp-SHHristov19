using Microsoft.EntityFrameworkCore;
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

            var student = context.Students
                .Include(s => s.Speciality)
                .ThenInclude(sp => sp.Faculties)
                .ThenInclude(f => f.Dean)
                .ThenInclude(d => d.User)
                .FirstOrDefault(s => s.Id == studentId);

            var facultyDean = student?.Speciality?.Faculties?
                .Select(f => f.Dean)
                .FirstOrDefault(d => d != null)
                ?.User;

            return $"{facultyDean?.FirstName} {facultyDean?.MiddleName} {facultyDean?.LastName}";
        }
    }
}
