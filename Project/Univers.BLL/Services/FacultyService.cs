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
    public class FacultyService
    {
        private readonly FacultyRepository _facultyRepository;
        private readonly Univers.Utilities.Utilities _utilities;

        public FacultyService()
        {
            _facultyRepository = new FacultyRepository();
            _utilities = new Univers.Utilities.Utilities();
        }

        /// <summary>
        /// Transfer data from Faculty entity to Faculty model
        /// </summary>
        /// <returns></returns>
        public List<FacultyModel> TransferDataFromEntityToModel()
        {
            List<FacultyModel> models = new();

            List<Faculty> entities = _facultyRepository.ReadAllData();

            foreach (var entity in entities)
            {
                var newModel = new FacultyModel();

                newModel.Id = entity.Id;
                newModel.Name = entity.Name;
                newModel.DeanId = entity.DeanId;
                newModel.ViceDeanId = entity.ViceDeanId;
                newModel.UniversityId = entity.UniversityId;
                newModel.Code = entity.Code;

                models.Add(newModel);
            }

            return models;
        }

        public List<FacultyModel> GetFacultiesByUniversityId(string universityId)
        {
            List<FacultyModel> faculties = TransferDataFromEntityToModel();

            var result = from faculty in faculties
                         where faculty.UniversityId == universityId
                         select faculty;

            return result.ToList();
        }

        public string GetFacultyCode(string facultyId)
        {
            var faculties = TransferDataFromEntityToModel();

            return faculties.FirstOrDefault(x => x.Id == facultyId).Code;
        }

        public string? GetDeanNameByStudentId(string studentId)
        {
            return _facultyRepository.GetDeanNameByStudentId(studentId);
        }

        public FacultyModel GetFacultyById(string FacultyId)
        {
            return TransferDataFromEntityToModel().FirstOrDefault(x => x.Id == FacultyId);
        }
    }
}
