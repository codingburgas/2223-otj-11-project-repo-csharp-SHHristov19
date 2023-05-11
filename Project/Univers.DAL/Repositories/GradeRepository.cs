using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.Models.Models;

namespace Univers.DAL.Repositories
{
    public class GradeRepository
    {
        public List<GradeModel> GetGradesByStudentId(string studentId)
        {
            using Context.Context context = new();

            return (from grade in context.Grades
                    join exam in context.Exams on grade.ExamId equals exam.Id
                    join subject in context.Subjects on exam.SubjectId equals subject.Id
                    join examSession in context.ExamSessions on exam.ExamSessionId equals examSession.Id
                    join semester in context.Semesters on examSession.SemesterId equals semester.Id
                    where grade.StudentId == studentId
                    select new GradeModel
                    {
                        Semester = semester.Number,
                        Subject = subject.Name,
                        Grade = grade.Grade1
                    }).ToList();
        }
    }
}
