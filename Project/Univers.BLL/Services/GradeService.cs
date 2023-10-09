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
    public class GradeService
    {
        private readonly GradeRepository _gradeRepository;

        public GradeService()
        {
            _gradeRepository = new GradeRepository();
        }

        /// <summary>
        /// Adds a grade for a specific student and exam.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="id">The ID of the exam.</param>
        /// <param name="grade">The grade to be added.</param>
        public void AddGrade(string? studentId, string? id, int? grade)
        {
            _gradeRepository.AddGrade(studentId, id, grade);
        }

        /// <summary>
        /// Edits the grade of a specific student for a particular subject.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="subjectId">The ID of the subject.</param>
        /// <param name="newGrade">The new grade to be updated.</param>
        public void EditGrade(string studentId, string subjectId, int? newGrade)
        {
            _gradeRepository.EditGrade(studentId, subjectId, newGrade);
        }

        /// <summary>
        /// Retrieves a list of StudentGradesModel objects for all students who have the specified subject ID.
        /// </summary>
        /// <param name="subjectId">The ID of the subject.</param>
        /// <returns>A list of StudentGradesModel objects representing the students and their grades for the subject.</returns>
        public List<StudentGradesModel> GetAllStudentsThatHasTheSubjectBySubjectId(string subjectId)
        {
            return _gradeRepository.GetAllStudentsThatHasTheSubjectBySubjectId(subjectId);
        }

        /// <summary>
        /// Retrieves a list of GradeModel objects representing the grades of a specific student.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>A list of GradeModel objects representing the grades of the student.</returns>
        public List<GradeModel> GetGradesByStudentId(string studentId)
        {
            return _gradeRepository.GetGradesByStudentId(studentId);
        }
    }
}
