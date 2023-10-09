using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Context;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class ExamRepository
    {
        /// <summary>
        /// Retrieves all data about an exam based on the provided exam ID.
        /// </summary>
        /// <param name="examId">The ID of the exam.</param>
        /// <returns>An Exam object containing all data about the exam, or null if not found.</returns>
        public Exam GetAllDataAboutExam(string examId)
        {
            using Context.Context context = new();

            return context.Exams.FirstOrDefault(x => x.Id == examId);
        }

        /// <summary>
        /// Retrieves exam information for a given exam session ID.
        /// </summary>
        /// <param name="examSessionId">The ID of the exam session.</param>
        /// <returns>An IEnumerable of Dictionary objects representing the exam information.</returns>
        public IEnumerable<Dictionary<string, string>> GetExamInfo(string examSessionId)
        {  
            using Context.Context context = new();

            var query = from exam in context.Exams
                        join subject in context.Subjects on exam.SubjectId equals subject.Id
                        join staff in context.Staff on exam.ProctorId equals staff.Id
                        join user in context.Users on staff.UserId equals user.Id
                        where exam.ExamSessionId == examSessionId
                        select new
                        {
                            SubjectName = subject.Name,
                            SubjectId = subject.Id,
                            ProctorName = $"{user.FirstName} {user.LastName}",
                            ProctorId = user.Id,
                            exam.Id
                        };
             
            var result = query.Select(x => new Dictionary<string, string>
            {
                { "subjectName", x.SubjectName },
                { "proctorName", x.ProctorName },
                { "subjectId", x.SubjectId },
                { "proctorId", x.ProctorId },
                { "examId", x.Id },
            }).ToList();

            return result.AsEnumerable();
        }

        /// <summary>
        /// Retrieves the exam session type information for a given exam session ID.
        /// </summary>
        /// <param name="examSessionId">The ID of the exam session.</param>
        /// <returns>An IEnumerable of Dictionary objects representing the exam session type information.</returns>
        public IEnumerable<Dictionary<string, string>> GetExamSessionTypeById(string examSessionId)
        {
            using Context.Context context = new();

            var query = from exam in context.Exams
                        join examSession in context.ExamSessions on exam.ExamSessionId equals examSession.Id
                        where exam.ExamSessionId == examSessionId
                        select new
                        { 
                            examSession.Type,
                            exam.Id
                        };


            return query.Select(x => new Dictionary<string, string>
            { 
                { "examId", x.Id },
                { "examSessionType", x.Type},
            }).ToList().AsEnumerable();
        }
    }
}
