using Microsoft.EntityFrameworkCore;
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

            return context.Students
                .Where(s => s.Id == studentId)
                .Include(s => s.Speciality)
                .ThenInclude(sp => sp.Faculties)
                .ThenInclude(f => f.University)
                .ThenInclude(u => u.Rector)
                .Select(s => $"{s.Speciality.Faculties.FirstOrDefault().University.Rector.User.FirstName} {s.Speciality.Faculties.FirstOrDefault().University.Rector.User.MiddleName} {s.Speciality.Faculties.FirstOrDefault().University.Rector.User.LastName}")
                .FirstOrDefault(); 
        }

        public string? GetUniversityNameByStudentId(string studentId)
        {
            using Context.Context context = new();

            var university = context.Students
                .Include(s => s.Speciality)
                    .ThenInclude(sp => sp.Faculties)
                        .ThenInclude(f => f.University)
                .Where(s => s.Id == studentId)
                .Select(s => s.Speciality.Faculties.FirstOrDefault().University.Name)
                .FirstOrDefault();

            return university;
        }

        public string? GetUniversityAddressByStudentId(string studentId)
        {
            using Context.Context context = new();

            var address = context.Students
                .Include(s => s.Speciality)
                    .ThenInclude(sp => sp.Faculties)
                        .ThenInclude(f => f.University)
                .Where(s => s.Id == studentId)
                .Select(s => s.Speciality.Faculties.FirstOrDefault().University.Address)
                .FirstOrDefault();

            return address;
        }
    }
}
