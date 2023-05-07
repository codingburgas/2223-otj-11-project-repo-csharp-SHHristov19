using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Context;

namespace Univers.DAL.Repositories
{
    public class SemesterRepository
    {
        public string? GetSemesterIdByStudentId(string studentId)
        {
            using Context.Context context = new();
            return (from es in context.ExamSessions
                   join s in context.Semesters on es.SemesterId equals s.Id
                   join sc in context.StudentCourses on s.Id equals sc.SemesterId
                   where sc.StudentId == studentId
                   select es.Id).FirstOrDefault();
        }
    }
}
