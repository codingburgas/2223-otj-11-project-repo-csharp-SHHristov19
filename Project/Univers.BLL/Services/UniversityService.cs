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
        private readonly List<UniversityModel> _universities;

        public UniversityService()
        {
            _universityRepository = new UniversityRepository();
            _universities = TransferDataFromEntityToModel();
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
            return _universities.FirstOrDefault(university => university.Id == id);
        }
    }
}
