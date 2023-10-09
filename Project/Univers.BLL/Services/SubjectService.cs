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

        /// <summary>
        /// Maps a Subject entity to a SubjectModel object.
        /// </summary>
        /// <param name="entity">The Subject entity to be mapped.</param>
        /// <returns>A SubjectModel object representing the mapped subject.</returns>
        public SubjectModel MapSubjectEntity(Subject entity)
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

        /// <summary>
        /// Retrieves a list of SubjectModel objects representing all subjects with exams for a given speciality ID.
        /// </summary>
        /// <param name="specialityId">The ID of the speciality.</param>
        /// <returns>A list of SubjectModel objects representing the subjects with exams for the given speciality ID.</returns>
        public List<SubjectModel> GetAllSubjectsWithExamBySpecialityId(string specialityId)
        {
            return _subjectRepository.GetAllSubjectsBySpecialityId(specialityId);
        }

        /// <summary>
        /// Retrieves a list of SubjectModel objects representing all subjects with exams for a given student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>A list of SubjectModel objects representing the subjects with exams for the given student ID.</returns>
        public List<SubjectModel> GetAllSubjectsWithExamByStudentId(string studentId)
        {
            return _subjectRepository.GetAllSubjectsBySpecialityId(_studentService.GetStudentById(studentId).SpecialityId);
        }

        /// <summary>
        /// Retrieves a list of SubjectModel objects representing all electives for a given student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>A list of SubjectModel objects representing the electives for the given student ID.</returns>
        public List<SubjectModel> GetAllElectivesByStudentId(string studentId)
        {
            return _subjectRepository.GetAllElectivesBySpecialityId(_studentService.GetStudentById(studentId).SpecialityId);
        }

        /// <summary>
        /// Retrieves a list of SubjectModel objects representing all subjects belonging to a teacher identified by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user (teacher).</param>
        /// <returns>A list of SubjectModel objects representing the subjects belonging to the teacher.</returns>
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

        /// <summary>
        /// Retrieves a SubjectModel object by its ID.
        /// </summary>
        /// <param name="subjectId">The ID of the subject.</param>
        /// <returns>A SubjectModel object representing the subject, or null if not found.</returns>
        public SubjectModel? GetSubjectById(string subjectId)
        {
            return TransferDataFromEntityToModel().FirstOrDefault(x => x.Id == subjectId);
        }

        /// <summary>
        /// Adds a new subject with the provided details to the system.
        /// </summary>
        /// <param name="teacherId">The ID of the teacher assigned to the subject (optional).</param>
        /// <param name="subjectName">The name of the subject (optional).</param>
        /// <param name="subjectCredits">The credit value of the subject (optional).</param>
        /// <param name="specialityId">The ID of the speciality associated with the subject.</param>
        public void AddSubject(string? teacherId, string? subjectName, decimal? subjectCredits, string specialityId)
        {
            _subjectRepository.AddSubject(teacherId, subjectName, subjectCredits, specialityId);
        }

        /// <summary>
        /// Deletes a subject from the system based on the provided subject ID.
        /// </summary>
        /// <param name="chosenSubjectId">The ID of the subject to be deleted.</param>
        public void DeleteSubject(string chosenSubjectId)
        {
            _subjectRepository.DeleteSubject(chosenSubjectId);
        }
    }
}
