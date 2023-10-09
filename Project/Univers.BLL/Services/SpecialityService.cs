using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;
using Univers.DAL.Repositories;
using Univers.Models.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Univers.BLL.Services
{
    public class SpecialityService
    {
        private readonly SpecialityRepository _specialityRepository;
        private readonly StaffService _staffService;
        private readonly Univers.Utilities.Utilities _utilities;

        public SpecialityService()
        {
            _specialityRepository = new SpecialityRepository();
            _utilities = new Univers.Utilities.Utilities();
            _staffService = new StaffService();
        }

        /// <summary>
        /// Transfer data from Speciality entity to Speciality model
        /// </summary>
        /// <returns></returns>
        public List<SpecialityModel> TransferDataFromEntityToModel()
        {
            List<SpecialityModel> models = new();

            List<Speciality> entities = _specialityRepository.ReadAllData();

            foreach (var entity in entities)
            { 
                models.Add(MapSpeciallityEntity(entity));
            }

            return models;
        }

        /// <summary>
        /// Maps a Speciality entity to a SpecialityModel.
        /// </summary>
        /// <param name="entity">The Speciality entity to be mapped.</param>
        /// <returns>A SpecialityModel representing the mapped entity.</returns>
        public SpecialityModel MapSpeciallityEntity(Speciality entity)
        {
            var newModel = new SpecialityModel();

            newModel.Id = entity.Id;
            newModel.Name = entity.Name;
            newModel.Degree = entity.Degree;
            newModel.Code = entity.Code;
            newModel.TutorId = entity.TutorId;
            newModel.Semesters = entity.Semesters;

            return newModel;
        }

        /// <summary>
        /// Retrieves a list of SpecialityModel objects representing specialities associated with a specific faculty ID and degree.
        /// </summary>
        /// <param name="facultyId">The ID of the faculty.</param>
        /// <param name="degree">The degree of the specialities (e.g., undergraduate, postgraduate).</param>
        /// <returns>A list of SpecialityModel objects representing the specialities.</returns>
        public List<SpecialityModel> GetSpecialitiesByFacultyId(string facultyId, string degree)
        {
            var models = new List<SpecialityModel>();
            var entities = _specialityRepository.GetSpecialitiesByFacultyId(facultyId, degree);

            foreach (var entity in entities)
            {
                models.Add(MapSpeciallityEntity(entity));
            } 

            return models;
        }

        /// <summary>
        /// Retrieves the speciality code for a specific speciality ID.
        /// </summary>
        /// <param name="specialityId">The ID of the speciality.</param>
        /// <returns>The speciality code as a string, or null if not found.</returns>
        public string? GetSpecialityCode(string specialityId)
        {
            var specialities = TransferDataFromEntityToModel();

            return specialities.FirstOrDefault(x => x.Id == specialityId).Code;
        }

        /// <summary>
        /// Retrieves the name of the speciality associated with a specific student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The name of the speciality as a string, or null if not found.</returns>
        public string? GetSpecialityNameByStudentId(string studentId)
        {
            return _specialityRepository.GetSpecialityNameByStudentId(studentId);
        }
         
        /// <summary>
        /// Retrieves the name of the tutor associated with a specific student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The name of the tutor as a string, or null if not found.</returns>
        public string? GetTutorNameByStudentId(string studentId)
        {
            return _specialityRepository.GetTutorNameByStudentId(studentId);
        }

        /// <summary>
        /// Retrieves the degree associated with a specific student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The degree as a string, or null if not found.</returns>
        public string? GetDegreeByStudentId(string studentId)
        {
            return _specialityRepository.GetDegreeByStudentId(studentId);
        }

        /// <summary>
        /// Retrieves a SpecialityModel for a specific speciality ID.
        /// </summary>
        /// <param name="specialityId">The ID of the speciality.</param>
        /// <returns>A SpecialityModel representing the speciality, or null if not found.</returns>
        public SpecialityModel? GetSpecialitiesById(string specialityId)
        {
            return TransferDataFromEntityToModel().FirstOrDefault(x => x.Id == specialityId);
        }

        /// <summary>
        /// Adds a new speciality using the provided AddSpecialityModel.
        /// </summary>
        /// <param name="addSpeciality">The AddSpecialityModel containing the details of the speciality to be added.</param>
        public void AddSpeciality(AddSpecialityModel? addSpeciality)
        {
            addSpeciality.TutorId = _staffService.GetStaffByUserId(addSpeciality.TutorId).Id;
            _specialityRepository.AddSpeciality(addSpeciality);
        }

        /// <summary>
        /// Deletes a speciality with the specified speciality ID.
        /// </summary>
        /// <param name="chosenSpecialityId">The ID of the speciality to be deleted.</param>
        public void DeleteSpeciality(string chosenSpecialityId)
        {
            _specialityRepository.DeleteSpeciality(chosenSpecialityId);
        }
    }
}
