using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Repositories;

namespace Univers.BLL.Services
{
    public class ExamSessionService
    {
        private readonly ExamSessionRepository _examSessionRepository;

        public ExamSessionService()
        {
            _examSessionRepository = new ExamSessionRepository();
        } 
    }
}
