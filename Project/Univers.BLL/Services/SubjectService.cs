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
                SubjectModel newModel = MapSubjectEntity(entity);

                models.Add(newModel);
            }

            return models;
        }

        private static SubjectModel MapSubjectEntity(Subject entity)
        {
            var newModel = new SubjectModel()
            {
                Id = entity.Id,
                TeacherId = entity.TeacherId,
                SpecialityId = entity.SpecialityId,
                Name = entity.Name,
                Type = entity.Type,
                Credits = (decimal)entity.Credits,
                List = entity.List,
            };

            return newModel;
        }

        public List<SubjectModel> GetAllSubjectsWithExamBySpecialityId(string specialityId)
        {
            return _subjectRepository.GetAllSubjectsBySpecialityId(specialityId);
        }

        public List<SubjectModel> GetAllSubjectsWithExamByStudentId(string studentId)
        {
            return _subjectRepository.GetAllSubjectsBySpecialityId(_studentService.GetStudentById(studentId).SpecialityId);
        }

        public List<SubjectModel> GetAllElectivesByStudentId(string studentId)
        {
            return _subjectRepository.GetAllElectivesBySpecialityId(_studentService.GetStudentById(studentId).SpecialityId);
        }

        public List<SubjectModel> GetAllSubjectsBelongingToTeacherByUserId(string userId)
        {
            var subjects = _subjectRepository.GetAllSubjectsBelongingToTeacherByUserId(userId);

            var result = new List<SubjectModel>();

            foreach (var subject in subjects)
            {
                SubjectModel newModel = MapSubjectEntity(subject);

                result.Add(newModel);
            }

            return result;
        }

        public SubjectModel? GetSubjectById(string subjectId)
        {
            return TransferDataFromEntityToModel().FirstOrDefault(x => x.Id == subjectId);
        }
    }
}
