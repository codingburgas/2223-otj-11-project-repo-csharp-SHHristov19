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
                models.Add(MapingEntity(entity));
            }

            return models;
        }

        public SpecialityModel MapingEntity(Speciality entity)
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

        public List<SpecialityModel> GetSpecialitiesByFacultyId(string facultyId, string degree)
        {
            var models = new List<SpecialityModel>();
            var entities = _specialityRepository.GetSpecialitiesByFacultyId(facultyId, degree);

            foreach (var entity in entities)
            {
                models.Add(MapingEntity(entity));
            } 

            return models;
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

        public SpecialityModel? GetSpecialitiesById(string specialityId)
        {
            return TransferDataFromEntityToModel().FirstOrDefault(x => x.Id == specialityId);
        }

        public void AddSpeciality(AddSpecialityModel? addSpeciality)
        {
            addSpeciality.TutorId = _staffService.GetStaffByUserId(addSpeciality.TutorId).Id;
            _specialityRepository.AddSpeciality(addSpeciality);
        }
    }
}
