using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;
using Univers.DAL.Repositories;
using Univers.Models.Models;

namespace Univers.BLL.Services
{
    public class StudentCourseService
    {
        private readonly StudentCourseRepository _studentCourseRepository; 

        public StudentCourseService()
        {
            _studentCourseRepository = new StudentCourseRepository(); 
        }

        /// <summary>
        /// Transfer data from User entity to User model
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StudentCourseModel> TransferDataFromEntityToModel()
        {
            var entities = _studentCourseRepository.ReadAllData();

            foreach (var entity in entities)
            {
                StudentCourseModel newModel = MapStudentCourseEntity(entity);

                yield return newModel;
            }
        }

        /// <summary>
        /// Maps a StudentCourse entity to a StudentCourseModel.
        /// </summary>
        /// <param name="entity">The StudentCourse entity to be mapped.</param>
        /// <returns>A StudentCourseModel representing the mapped entity.</returns>
        public StudentCourseModel MapStudentCourseEntity(StudentCourse entity)
        {
            var newModel = new StudentCourseModel();

            newModel.StudentId = entity.StudentId; 
            newModel.Course = entity.Course;
            newModel.SemesterId = entity.SemesterId;

            return newModel;
        }

        /// <summary>
        /// Retrieves the course information for a specific student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The course information as a string.</returns>
        public string GetStudentCourseByStudentId(string studentId)
        {
            var studentCourses = TransferDataFromEntityToModel();
            return studentCourses.FirstOrDefault(x => x.StudentId == studentId).Course.ToString();
        }

        /// <summary>
        /// Adds a default student course for a specific student ID, education, and university ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="education">The education level of the student.</param>
        /// <param name="universityId">The ID of the university.</param>
        public void AddStudentCourseByDefault(string studentId, string education, string universityId)
        {
            _studentCourseRepository.AddStudentCourseByLoginStudent(studentId, education.ToLower(), universityId);
        }
    }
}
