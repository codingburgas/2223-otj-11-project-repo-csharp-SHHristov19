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
    public class SemesterService
    {
        private SemesterRepository _semesterRepository;

        public SemesterService()
        {
            _semesterRepository = new SemesterRepository();
        }

        /// <summary>
        /// Transfers data from entity to model for Semesters.
        /// </summary>
        /// <returns>A list of SemesterModel objects representing the transferred data.</returns>
        public List<SemesterModel> TransferDataFromEntityToModel()
        {
            List<SemesterModel> models = new();

            List<Semester> entities = _semesterRepository.ReadAllData();

            foreach (var entity in entities)
            {
                models.Add(MapSemesterEntity(entity));
            }

            return models;
        }

        /// <summary>
        /// Maps a Semester entity to a SemesterModel.
        /// </summary>
        /// <param name="entity">The Semester entity to be mapped.</param>
        /// <returns>A SemesterModel representing the mapped entity.</returns>
        public SemesterModel MapSemesterEntity(Semester entity)
        {
            var newModel = new SemesterModel();

            newModel.Id = entity.Id;
            newModel.Number = entity.Number;
            newModel.UniversityId = entity.UniversityId;
            newModel.Type = entity.Type;
            newModel.DateOfStart = entity.DateOfStart;
            newModel.DateOfEnd = entity.DateOfEnd; 

            return newModel;
        }

        /// <summary>
        /// Adds a semester to a university using the provided SemesterModel.
        /// </summary>
        /// <param name="semester">The SemesterModel containing the details of the semester to be added.</param>
        public void AddSemesterInUniversityById(SemesterModel semester)
        {
            _semesterRepository.AddSemester(semester);
        }

        /// <summary>
        /// Retrieves all semesters associated with a specific university ID.
        /// </summary>
        /// <param name="chosenUniversityId">The ID of the chosen university.</param>
        /// <returns>An IEnumerable collection of SemesterModel objects representing the semesters.</returns>
        public IEnumerable<SemesterModel> GetAllSemestersByUniversityId(string chosenUniversityId)
        {
            return TransferDataFromEntityToModel().Where(x => x.UniversityId == chosenUniversityId).OrderBy(x => x.Number);
        }

        /// <summary>
        /// Deletes a semester with the specified semester ID.
        /// </summary>
        /// <param name="semesterId">The ID of the semester to be deleted.</param>
        public void DeleteSemesterById(string semesterId)
        {
            _semesterRepository.DeleteSemester(semesterId);
        }
    }
}
