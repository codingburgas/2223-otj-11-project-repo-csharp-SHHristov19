using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class ExamRepository
    {
        public Exam GetAllDataAboutExam(string examId)
        {
            using Context.Context context = new();

            return context.Exams.FirstOrDefault(x => x.Id == examId);
        }

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
    }
}
