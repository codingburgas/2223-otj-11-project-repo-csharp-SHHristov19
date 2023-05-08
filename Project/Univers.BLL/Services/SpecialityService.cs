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
        private readonly FacultySpecialityService _facultySpecialityService;
        private readonly Univers.Utilities.Utilities _utilities;

        public SpecialityService()
        {
            _specialityRepository = new SpecialityRepository();
            _utilities = new Univers.Utilities.Utilities();
            _facultySpecialityService = new FacultySpecialityService();
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
                newModel.Semesters = entity.Semesters; 
 
                models.Add(newModel);
            }

            return models;
        }

        public List<SpecialityModel> GetSpecialitiesByFacultyId(string facultyId, string degree)
        {
            List<FacultySpecialityModel> facultySpecialities = _facultySpecialityService.TransferDataFromEntityToModel();

            List<SpecialityModel> specialities = TransferDataFromEntityToModel();

            var result = from facultySpecialty in facultySpecialities
                         join speciality in specialities
                         on facultySpecialty.SpecialityId equals speciality.Id
                         where facultySpecialty.FacultyId == facultyId
                         select speciality;

            var res = from specialitiesForm in result
                      where specialitiesForm.Degree == degree
                      select specialitiesForm;

            return res.ToList();
        }

        public string? GetSpecialityCode(string specialityId)
        {
            var specialities = TransferDataFromEntityToModel();

            return specialities.FirstOrDefault(x => x.Id == specialityId).Code;
        }

        public string? GetSpecialityNameByStudentId(string studentId)
        {
            return _specialityRepository.GetSpecialityNameByStudentId(studentId);
        }

        public string? GetTutorNameByStudentId(string studentId)
        {
            return _specialityRepository.GetTutorNameByStudentId(studentId);
        }

        public string? GetDegreeByStudentId(string studentId)
        {
            return _specialityRepository.GetDegreeByStudentId(studentId);
        }
    }
}
