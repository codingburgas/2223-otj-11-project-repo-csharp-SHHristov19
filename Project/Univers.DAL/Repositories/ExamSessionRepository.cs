using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.DAL.Repositories
{
    public class ExamSessionRepository
    {
        /// <summary>
        /// Retrieves the exam session ID associated with a given student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The exam session ID associated with the student, or null if not found.</returns>
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
