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
    }
}
