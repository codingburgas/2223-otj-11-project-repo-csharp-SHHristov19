using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;
using Univers.DAL.Repositories;
using Univers.Models.Models;

namespace Univers.BLL.Services
{
    public class FacultySpecialityService
    {
        private readonly FacultySpecialityRepository _facultySpecialityRepository;
        private readonly Univers.Utilities.Utilities _utilities;

        public FacultySpecialityService()
        {
            _facultySpecialityRepository = new FacultySpecialityRepository();
            _utilities = new Univers.Utilities.Utilities();
        }

        /// <summary>
        /// Transfer data from FacultySpeciality entity to FacultySpeciality model
        /// </summary>
        /// <returns></returns>
        public List<FacultySpecialityModel> TransferDataFromEntityToModel()
        {
            List<FacultySpecialityModel> models = new();

            List<FacultySpeciality> entities = _facultySpecialityRepository.ReadAllData();

            foreach (var entity in entities)
            {
                var newModel = new FacultySpecialityModel();

                newModel.FacultyId = entity.FacultyId;
                newModel.SpecialityId = entity.SpecialityId;

                models.Add(newModel);
            }

            return models;
        }
    }
}
