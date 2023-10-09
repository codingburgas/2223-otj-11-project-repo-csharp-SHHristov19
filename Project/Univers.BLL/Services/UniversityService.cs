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
    public class UniversityService
    {
        private readonly UniversityRepository _universityRepository; 
        private readonly StaffService _staffService; 

        public UniversityService()
        {
            _universityRepository = new UniversityRepository(); 
            _staffService = new StaffService();
        }

        /// <summary>
        /// Transfer data from University entity to University model
        /// </summary>
        /// <returns></returns>
        public List<UniversityModel> TransferDataFromEntityToModel()
        {
            List<UniversityModel> models = new();

            var entities = _universityRepository.ReadAllData();

            foreach (var entity in entities)
            {
                UniversityModel newModel = MapUniversityEntity(entity);

                models.Add(newModel);
            }

            return models;
        }

        /// <summary>
        /// Maps a University entity to a UniversityModel object.
        /// </summary>
        /// <param name="entity">The University entity to be mapped.</param>
        /// <returns>A UniversityModel object representing the mapped university.</returns>
        public UniversityModel MapUniversityEntity(University entity)
        {
            var newModel = new UniversityModel();

            newModel.Id = entity.Id;
            newModel.Name = entity.Name;
            newModel.RectorId = entity.RectorId;
            newModel.Address = entity.Address;
            newModel.Capacity = entity.Capacity;
            return newModel;
        }

        /// <summary>
        /// Retrieves a UniversityModel object by its ID.
        /// </summary>
        /// <param name="id">The ID of the university.</param>
        /// <returns>A UniversityModel object representing the university, or null if not found.</returns>
        public UniversityModel GetUniversityById(string id)
        {
            return TransferDataFromEntityToModel().FirstOrDefault(university => university.Id == id);
        }

        /// <summary>
        /// Retrieves the rector associated with the university of a given student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The rector associated with the university of the student, or null if not found.</returns>
        public string? GetRectorByStudentId(string studentId)
        {
            return _universityRepository.GetRectorByStudentId(studentId);
        }

        /// <summary>
        /// Retrieves the university name associated with a given student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The university name associated with the student, or null if not found.</returns>
        public string? GetUniversityNameByStudentId(string studentId)
        {
            return _universityRepository.GetUniversityNameByStudentId(studentId);
        }

        /// <summary>
        /// Retrieves the university address associated with a given student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The university address associated with the student, or null if not found.</returns>
        public string? GetUniversityAddressByStudentId(string studentId)
        {
            return _universityRepository.GetUniversityAddressByStudentId(studentId);
        }

        /// <summary>
        /// Retrieves the rector associated with a given university ID.
        /// </summary>
        /// <param name="universityId">The ID of the university.</param>
        /// <returns>The rector associated with the university, or null if not found.</returns>
        public string? GetRectorByUniversityId(string universityId)
        {
            return _universityRepository.GetRectorNameByUniversityId(universityId);
        }

        /// <summary>
        /// Adds a new university to the system based on the provided university details.
        /// </summary>
        /// <param name="addUniversity">The UniversityModel object containing the details of the university to be added.</param>
        public void AddUniversity(UniversityModel? addUniversity)
        {
            addUniversity.RectorId = _staffService.GetStaffByUserId(addUniversity.Rector.UserId).Id;
            _universityRepository.AddUniversity(addUniversity);
        }

        /// <summary>
        /// Deletes a university from the system based on the provided university ID.
        /// </summary>
        /// <param name="chosenUniversityId">The ID of the university to be deleted.</param>
        public void DeleteUniversity(string chosenUniversityId)
        {
            _universityRepository.DeleteUniversity(chosenUniversityId);
        }
    }
}
