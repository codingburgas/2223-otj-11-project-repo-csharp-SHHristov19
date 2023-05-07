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

            var semesterId = context.Semesters.FirstOrDefault(x => x.UniversityId == universityId && x.Type == "Зимен " + education && x.Number == 1).Id;

            var studentCourse = new StudentCourse();

            studentCourse.StudentId = studentId;
            studentCourse.Course = 1;
            studentCourse.SemesterId = semesterId;

            context.StudentCourses.Add(studentCourse);
            context.SaveChanges();
        } 
    }
}
