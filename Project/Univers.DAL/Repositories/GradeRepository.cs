using Microsoft.EntityFrameworkCore;
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
    public class GradeRepository
    {
        private readonly UserRepository _userRepository;

        public GradeRepository()
        {
            _userRepository = new UserRepository();
        }

        /// <summary>
        /// Retrieves a list of GradeModel objects representing grades associated with a given student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>A list of GradeModel objects representing the grades associated with the student.</returns>
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

        /// <summary>
        /// Retrieves the grade for a specific subject and student.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="subjectId">The ID of the subject.</param>
        /// <returns>The GradeModel representing the grade for the subject and student, or null if not found.</returns>
        public static GradeModel? GetSubjectGrade(string studentId, string subjectId)
        {
            using Context.Context context = new();

            return (from g in context.Grades
                   join s in context.Students on g.StudentId equals s.Id
                   join e in context.Exams on g.ExamId equals e.Id
                   join sj in context.Subjects on e.SubjectId equals sj.Id
                   join es in context.ExamSessions on e.ExamSessionId equals es.Id
                   join se in context.Semesters on es.SemesterId equals se.Id
                   where s.Id == studentId && sj.Id == subjectId
                   select new GradeModel 
                   { 
                        Grade = g.Grade1,
                        StudentId = studentId,
                        Semester = se.Number
                   }).FirstOrDefault(); 
        }

        /// <summary>
        /// Retrieves a list of StudentGradesModel objects representing all students who have the subject with the specified subject ID.
        /// </summary>
        /// <param name="subjectId">The ID of the subject.</param>
        /// <returns>A list of StudentGradesModel objects representing the students who have the subject.</returns>
        public List<StudentGradesModel> GetAllStudentsThatHasTheSubjectBySubjectId(string subjectId)
        {
            using Context.Context context = new();

            return (from sp in context.Specialities
                    join s in context.Subjects on sp.Id equals s.SpecialityId 
                    join st in context.Students on sp.Id equals st.SpecialityId
                    where s.Id == subjectId
                    select new StudentGradesModel 
                    {
                        StudentId = st.Id,
                        Grade = GetSubjectGrade(st.Id, subjectId),
                        Student = _userRepository.GetUserModelByStudentId(st.Id),
                    })
                    .ToList();
        }

        /// <summary>
        /// Edits the grade for a specific student and subject.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="subjectId">The ID of the subject.</param>
        /// <param name="newGrade">The new grade to be assigned.</param>
        public void EditGrade(string studentId, string subjectId, int? newGrade)
        {
            using Context.Context context = new();

            var studentGrade = context.Grades
                            .Include(x => x.Student)
                            .Include(x => x.Exam)
                            .First(x => x.StudentId == studentId && x.Exam.SubjectId == subjectId);

            studentGrade.Grade1 = newGrade;

            context.Update(studentGrade);
            context.SaveChanges();
        }

        /// <summary>
        /// Adds a grade for a specific student and subject.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="id">The ID of the subject.</param>
        /// <param name="grade">The grade to be assigned.</param>
        public void AddGrade(string? studentId, string? id, int? grade)
        {
            using Context.Context context = new();

            var exam = context.Exams.Include(x => x.Subject).First(x => x.SubjectId == id);

            var studentGrade = new Grade()
            {
                Grade1 = grade,
                StudentId = studentId,
                ExamId = exam.Id,
            };

            context.Add(studentGrade);
            context.SaveChanges();
        }
    }
}
