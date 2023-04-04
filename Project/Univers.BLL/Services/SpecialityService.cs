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
    public class SpecialityService
    {
        private readonly SpecialityRepository _specialityRepository;
        private readonly Univers.Utilities.Utilities _utilities;

        public SpecialityService()
        {
            _specialityRepository = new SpecialityRepository();
            _utilities = new Univers.Utilities.Utilities();
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
                var newModel = new SpecialityModel();

                newModel.Id = entity.Id;
                newModel.Name = entity.Name;
                newModel.Degree = entity.Degree;
                newModel.Code = entity.Code;
                newModel.TutorId = entity.TutorId;

                models.Add(newModel);
            }

            return models;
        }

        public List<SpecialityModel> GetSpecialities()
        {

        }
    }
}
