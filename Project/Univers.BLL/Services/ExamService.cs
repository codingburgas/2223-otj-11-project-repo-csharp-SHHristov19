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
    public class ExamService
    {
        private readonly ExamRepository _examRepository;
        private readonly ExamSessionRepository _examSessionRepository;

        public ExamService()
        {
            _examRepository = new ExamRepository();
            _examSessionRepository = new ExamSessionRepository();
        }

        /// <summary>
        /// Maps an Exam entity to an ExamModel.
        /// </summary>
        /// <param name="entity">The Exam entity to be mapped.</param>
        /// <returns>An ExamModel representing the mapped entity.</returns>
        public ExamModel MapExamEntity(Exam entity)
        {
            var newModel = new ExamModel();

            newModel.Id = entity.Id;  
            newModel.ExamSessionId = entity.ExamSessionId;
            newModel.Location = entity.Location;
            newModel.TimeOfStart = entity.TimeOfStart;
            newModel.TimeOfEnd = entity.TimeOfEnd;

            return newModel;
        }

        /// <summary>
        /// Retrieves a list of ExamModel objects containing exam information for a specific student and semester.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>A list of ExamModel objects representing the exam information.</returns>
        public List<ExamModel> GetExamInfoBySemesterId(string studentId)
        {
            var examSessionId = _examSessionRepository.GetExamSessionIdByStudentId(studentId);
            var exams = _examRepository.GetExamInfo(examSessionId);
            var examTypes = _examRepository.GetExamSessionTypeById(examSessionId);

            List<ExamModel> examModels = new List<ExamModel>();

            foreach (var exam in exams)
            {
                ExamModel examModel = MapExamEntity(_examRepository.GetAllDataAboutExam(exam["examId"]));
                
                examModel.Subject = exam["subjectName"];
                examModel.Proctor = exam["proctorName"];
                examModel.SubjectId = exam["subjectId"];
                examModel.ProctorId = exam["proctorId"];

                foreach (var examType in examTypes)
                {
                    if (examModel.Id == examType["examId"])
                    {
                        examModel.Type = examType["examSessionType"];
                        break;
                    }
                }

                examModels.Add(examModel);
            }

            return examModels;
        }
    }
}
