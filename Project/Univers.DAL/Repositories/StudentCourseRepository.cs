using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class StudentCourseRepository
    { 
        /// <summary>
        /// Read the data from the Student table
        /// </summary>
        /// <returns></returns>
        public List<StudentCourse> ReadAllData()
        {
            using Context.Context context = new();

            return context.StudentCourses.ToList();
        } 
        
        public void AddStudentCourseByLoginStudent(string studentId, string education, string universityId)
        {
            using Context.Context context = new();

            var semester = context.Semesters.FirstOrDefault(x => x.UniversityId == universityId && x.Type == "Зимен " + education && x.Number == 1);

            var studentCourse = new StudentCourse
            {
                StudentId = studentId,
                Course = 1,
                SemesterId = semester?.Id
            };

            context.StudentCourses.Add(studentCourse);
            context.SaveChanges();
        } 
    }
}
