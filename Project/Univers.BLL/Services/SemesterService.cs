using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Repositories;
using Univers.Models.Models;

namespace Univers.BLL.Services
{
    public class SemesterService
    {
        private SemesterRepository _semesterRepository;

        public SemesterService()
        {
            _semesterRepository = new SemesterRepository();
        }

        public void AddSemesterInUniversityById(SemesterModel semester)
        {
            _semesterRepository.AddSemester(semester);
        }
    }
}
