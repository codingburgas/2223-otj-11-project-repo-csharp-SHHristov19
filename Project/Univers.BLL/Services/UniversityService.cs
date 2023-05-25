using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var newModel = new UniversityModel();

                newModel.Id = entity.Id;
                newModel.Name = entity.Name;
                newModel.RectorId = entity.RectorId;
                newModel.Address = entity.Address;
                newModel.Capacity = entity.Capacity;

                models.Add(newModel);
            }

            return models;
        } 

        public UniversityModel GetUniversityById(string id)
        {
            return TransferDataFromEntityToModel().FirstOrDefault(university => university.Id == id);
        }

        public string? GetRectorByStudentId(string studentId)
        {
            return _universityRepository.GetRectorByStudentId(studentId);
        }

        public string? GetUniversityNameByStudentId(string studentId)
        {
            return _universityRepository.GetUniversityNameByStudentId(studentId);
        }

        public string? GetUniversityAddressByStudentId(string studentId)
        {
            return _universityRepository.GetUniversityAddressByStudentId(studentId);
        }

        public string? GetRectorByUniversityId(string universityId)
        {
            return _universityRepository.GetRectorNameByUniversityId(universityId);
        }

        public void AddUniversity(UniversityModel? addUniversity)
        {
            addUniversity.RectorId = _staffService.GetStaffByUserId(addUniversity.Rector.UserId).Id;
            _universityRepository.AddUniversity(addUniversity);
        }

        public void DeleteUniversity(string chosenUniversityId)
        {
            _universityRepository.DeleteUniversity(chosenUniversityId);
        }
    }
}
