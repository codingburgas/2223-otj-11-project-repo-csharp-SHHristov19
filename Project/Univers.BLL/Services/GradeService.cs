using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void EditGrade(string studentId, string subjectId, int? newGrade)
        {
            _gradeRepository.EditGrade(studentId, subjectId, newGrade);
        }

        public List<StudentGradesModel> GetAllStudentsThatHasTheSubjectBySubjectId(string subjectId)
        {
            return _gradeRepository.GetAllStudentsThatHasTheSubjectBySubjectId(subjectId);
        }

        public List<GradeModel> GetGradesByStudentId(string studentId)
        {
            return _gradeRepository.GetGradesByStudentId(studentId);
        }
    }
}
