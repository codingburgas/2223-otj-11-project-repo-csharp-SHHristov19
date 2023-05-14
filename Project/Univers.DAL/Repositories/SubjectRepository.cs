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
    public class SubjectRepository
    {
        // Read the data from the Subject table
        public List<Subject> ReadAllData()
        {
            using Context.Context context = new();

            return context.Subjects.ToList();
        }

        public List<SubjectModel> GetAllSubjectsBySpecialityId(string specialityId)
        {
            using Context.Context context = new();

            return context.Subjects
                .Include(s => s.Teacher.User)
                .Include(ss => ss.Semesters)
                .Where(s => s.Type == "Предмет с изпит" && s.SpecialityId == specialityId)
                .OrderBy(s => s.Semesters.OrderBy(ss => ss.Number).FirstOrDefault().Number)
                .Select(s => new SubjectModel
                {
                    Number = s.Semesters.FirstOrDefault().Number,
                    Name = s.Name,
                    TeacherName = s.Teacher.User.FirstName + " " + s.Teacher.User.MiddleName + " " + s.Teacher.User.LastName,
                    Credits = s.Credits
                })
                .ToList();
        }
    }
}
