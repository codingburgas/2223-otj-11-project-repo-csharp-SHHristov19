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
        private readonly StaffService _staffService;
        private readonly Univers.Utilities.Utilities _utilities;

        public FacultyService()
        {
            _facultyRepository = new FacultyRepository();
            _utilities = new Univers.Utilities.Utilities();
            _staffService = new StaffService();
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
                FacultyModel newModel = MapFacultyEntity(entity);

                models.Add(newModel);
            }

            return models;
        }

        public FacultyModel MapFacultyEntity(Faculty entity)
        {
            var newModel = new FacultyModel();

            newModel.Id = entity.Id;
            newModel.Name = entity.Name;
            newModel.DeanId = entity.DeanId;
            newModel.ViceDeanId = entity.ViceDeanId;
            newModel.UniversityId = entity.UniversityId;
            newModel.Code = entity.Code;
            return newModel;
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

        public void AddFaculty(AddFacultyModel? addFaculty)
        {
            addFaculty.DeanId = _staffService.GetStaffByUserId(addFaculty.DeanId).Id;
            addFaculty.ViceDeanId = _staffService.GetStaffByUserId(addFaculty.ViceDeanId).Id;
            _facultyRepository.AddFaculty(addFaculty);
        }

        public void DeleteFaculty(string facultyId)
        {
            _facultyRepository.DeleteFaculty(facultyId);
        }
    }
}
