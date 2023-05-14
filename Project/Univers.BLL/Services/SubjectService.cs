using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Repositories;
using Univers.Models.Models;

namespace Univers.BLL.Services
{
    public class SubjectService
    {
        private readonly SubjectRepository _subjectRepository;
        private readonly StudentService _studentService;

        public SubjectService()
        {
            _subjectRepository = new SubjectRepository();
            _studentService = new StudentService();
        }

        /// <summary>
        /// Transfer data from Subject entity to Subject model
        /// </summary>
        /// <returns></returns>
        public List<SubjectModel> TransferDataFromEntityToModel()
        {
            List<SubjectModel> models = new();

            var entities = _subjectRepository.ReadAllData();

            foreach (var entity in entities)
            {
                var newModel = new SubjectModel();

                newModel.Id = entity.Id;
                newModel.TeacherId = entity.TeacherId;
                newModel.SpecialityId = entity.SpecialityId;
                newModel.Name = entity.Name;
                newModel.Type = entity.Type;
                newModel.Credits = (decimal)entity.Credits;
                newModel.List = entity.List;

                models.Add(newModel);
            }

            return models;
        }

        public List<SubjectModel> GetAllSubjectsWithExamBySpecialityId(string studentId)
        {
            return _subjectRepository.GetAllSubjectsBySpecialityId(_studentService.GetStudentById(studentId).SpecialityId);
        }

        public List<SubjectModel> GetAllElectivesBySpecialityId(string studentId)
        {
            return _subjectRepository.GetAllElectivesBySpecialityId(_studentService.GetStudentById(studentId).SpecialityId);
        }
    }
}
