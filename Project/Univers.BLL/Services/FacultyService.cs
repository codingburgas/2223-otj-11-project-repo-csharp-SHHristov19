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

        /// <summary>
        /// Maps a Faculty entity to a FacultyModel.
        /// </summary>
        /// <param name="entity">The Faculty entity to be mapped.</param>
        /// <returns>A FacultyModel representing the mapped entity.</returns>
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

        /// <summary>
        /// Retrieves a list of FacultyModel objects representing faculties associated with a specific university.
        /// </summary>
        /// <param name="universityId">The ID of the university.</param>
        /// <returns>A list of FacultyModel objects representing the faculties.</returns>
        public List<FacultyModel> GetFacultiesByUniversityId(string universityId)
        {
            List<FacultyModel> faculties = TransferDataFromEntityToModel();

            var result = from faculty in faculties
                         where faculty.UniversityId == universityId
                         select faculty;

            return result.ToList();
        }

        /// <summary>
        /// Retrieves the faculty code for a specific faculty ID.
        /// </summary>
        /// <param name="facultyId">The ID of the faculty.</param>
        /// <returns>The faculty code as a string.</returns>
        public string GetFacultyCode(string facultyId)
        {
            var faculties = TransferDataFromEntityToModel();

            return faculties.FirstOrDefault(x => x.Id == facultyId).Code;
        }

        /// <summary>
        /// Retrieves the name of the dean associated with a specific student ID.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <returns>The name of the dean as a string, or null if not found.</returns>
        public string? GetDeanNameByStudentId(string studentId)
        {
            return _facultyRepository.GetDeanNameByStudentId(studentId);
        }

        /// <summary>
        /// Retrieves a FacultyModel object for a specific faculty ID.
        /// </summary>
        /// <param name="facultyId">The ID of the faculty.</param>
        /// <returns>A FacultyModel object representing the faculty, or null if not found.</returns>
        public FacultyModel GetFacultyById(string FacultyId)
        {
            return TransferDataFromEntityToModel().FirstOrDefault(x => x.Id == FacultyId);
        }

        /// <summary>
        /// Adds a new faculty using the provided AddFacultyModel.
        /// </summary>
        /// <param name="addFaculty">The AddFacultyModel containing the details of the faculty to be added.</param>
        public void AddFaculty(AddFacultyModel? addFaculty)
        {
            addFaculty.DeanId = _staffService.GetStaffByUserId(addFaculty.DeanId).Id;
            if(addFaculty.ViceDeanId != null)
            {
                addFaculty.ViceDeanId = _staffService.GetStaffByUserId(addFaculty.ViceDeanId).Id;
            }
            _facultyRepository.AddFaculty(addFaculty);
        }

        /// <summary>
        /// Deletes a faculty with the specified faculty ID.
        /// </summary>
        /// <param name="facultyId">The ID of the faculty to be deleted.</param>
        public void DeleteFaculty(string facultyId)
        {
            _facultyRepository.DeleteFaculty(facultyId);
        }
    }
}
