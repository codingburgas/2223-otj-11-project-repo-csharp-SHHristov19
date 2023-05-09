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

        public List<GradeModel> GetGradesByStudentId(string studentId)
        {
            return _gradeRepository.GetGradesByStudentId(studentId);
        }
    }
}
