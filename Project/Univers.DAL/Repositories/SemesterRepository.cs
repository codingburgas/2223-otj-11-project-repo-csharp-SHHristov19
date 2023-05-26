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

        public void DeleteSemester(string semesterId)
        {
            using Context.Context context = new();

            var semester = context.Semesters
                                  .Include(s => s.Subjects) 
                                      .ThenInclude(s => s.Exams) 
                                          .ThenInclude(e => e.Grades)
                                  .Include(s => s.Subjects)
                                      .ThenInclude(s => s.SubjectComponents)  
                                  .Include(e => e.ExamSessions)
                                      .ThenInclude(e => e.Exams)
                                          .ThenInclude(e => e.Grades)
                                  .Include(c => c.StudentCourses)
                                  .FirstOrDefault(x => x.Id == semesterId);

            semester.StudentCourses.Clear();

            foreach (var subject in semester.Subjects)
            {
                foreach (var exam in subject.Exams)
                {
                    exam.Grades.Clear();
                }

                subject.Exams.Clear();
            }

            semester.Subjects.Clear();

            foreach (var subject in semester.ExamSessions)
            {
                foreach (var exam in subject.Exams)
                {
                    exam.Grades.Clear();
                }

                subject.Exams.Clear();
            }

            semester.ExamSessions.Clear();

            context.Semesters.Remove(semester);
            context.SaveChanges();
        }
    }
}
