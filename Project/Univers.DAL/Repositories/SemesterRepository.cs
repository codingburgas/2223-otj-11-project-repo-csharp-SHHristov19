using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Context;
using Univers.DAL.Entities;
using Univers.Models.Models;

namespace Univers.DAL.Repositories
{
    public class SemesterRepository
    {
        public List<Semester> ReadAllData()
        {
            using Context.Context context = new();

            return context.Semesters.ToList();
        }

        public void AddSemester(SemesterModel semester)
        {
            using Context.Context context = new();

            var newSemester = new Semester()
            { 
                Id = Guid.NewGuid().ToString("D"),
                Number = semester.Number,
                Type = semester.Type,
                DateOfStart = semester.DateOfStart,
                DateOfEnd = semester.DateOfEnd,
                UniversityId = semester.UniversityId,
            };

            context.Semesters.Add(newSemester); 
            context.SaveChanges();
        } 
    }
}
