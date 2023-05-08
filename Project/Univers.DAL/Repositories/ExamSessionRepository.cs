using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.DAL.Repositories
{
    public class ExamSessionRepository
    {
        public string? GetExamSessionIdByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from examSession in context.ExamSessions
                    join semester in context.Semesters on examSession.SemesterId equals semester.Id
                    join studentCourse in context.StudentCourses on semester.Id equals studentCourse.SemesterId
                    where studentCourse.StudentId == studentId
                    select examSession.Id).FirstOrDefault();
        }
    }
}
