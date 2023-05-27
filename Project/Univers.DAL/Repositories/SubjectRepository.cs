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

            return (from s in context.Subjects
                    join sf in context.Staff on s.TeacherId equals sf.Id
                    join u in context.Users on sf.UserId equals u.Id
                    where s.Type == "Предмет с изпит" && s.SpecialityId == specialityId
                    orderby s.Name
                    select new SubjectModel
                    {
                        Name = s.Name,
                        Id = s.Id,
                        TeacherName = u.FirstName + " " + u.MiddleName + " " + u.LastName,
                        Credits = s.Credits,
                        List = s.List
                    })
                    .ToList();
        }

        public List<SubjectModel> GetAllElectivesBySpecialityId(string specialityId)
        {
            using Context.Context context = new();

            return (from s in context.Subjects
                   join sf in context.Staff on s.TeacherId equals sf.Id
                   join u in context.Users on sf.UserId equals u.Id
                   where s.Type == "Предмет с текуща оценка" && s.SpecialityId == specialityId
                   orderby s.Name
                   select new SubjectModel
                   {
                       Name = s.Name,
                       TeacherName = u.FirstName + " " + u.MiddleName + " " + u.LastName,
                       Credits = s.Credits,
                       List = s.List
                   })
                   .ToList();
        }

        public List<Subject> GetAllSubjectsBelongingToTeacherByUserId(string userId)
        {
            using Context.Context context = new();

            return (from staff in context.Staff
                    join subject in context.Subjects on staff.Id equals subject.TeacherId
                    join user in context.Users on staff.UserId equals user.Id
                    where user.IsConfirmed == true && user.Id == userId
                    select subject).ToList();
        }

        public void AddSubject(string? teacherId, string? subjectName, decimal? subjectCredits, string specialityId)
        {
            using Context.Context context = new();

            var subject = new Subject
            {
                Id = Guid.NewGuid().ToString("D"),
                Name = subjectName,
                Type = "Предмет с изпит",
                List = 0,
                Credits = subjectCredits,
                TeacherId = teacherId,
                SpecialityId = specialityId,
            };

            context.Subjects.Add(subject);
            context.SaveChanges();
        }

        public void DeleteSubject(string chosenSubjectId)
        {
            using Context.Context context = new();

            var subject = context.Subjects
                .Include(x => x.Semesters)
                    .ThenInclude(x => x.ExamSessions)
                        .ThenInclude(x => x.Exams)
                            .ThenInclude(x => x.Grades)
                .Include(x => x.Semesters)
                    .ThenInclude(x => x.StudentCourses)
                .Include(x => x.Exams)
                    .ThenInclude(x => x.Grades)
                .FirstOrDefault(x => x.Id == chosenSubjectId);

            foreach (var semester in subject.Semesters)
            {
                foreach (var examSession in semester.ExamSessions)
                {
                    foreach (var exam in examSession.Exams)
                    {
                        exam.Grades.Clear();
                    }
                    examSession.Exams.Clear();
                }
                semester.ExamSessions.Clear();
                semester.StudentCourses.Clear();
            }
            subject.Semesters.Clear();
             

            foreach (var exam in subject.Exams)
            {
                exam.Grades.Clear();
            }

            subject.Exams.Clear();

            context.Subjects.Remove(subject);
            context.SaveChanges(); 
        }
    }
}
