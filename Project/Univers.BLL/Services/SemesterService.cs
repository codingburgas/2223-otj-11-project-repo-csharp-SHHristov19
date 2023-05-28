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

        public void AddSemesterInUniversityById(SemesterModel semester)
        {
            _semesterRepository.AddSemester(semester);
        }

        public IEnumerable<SemesterModel> GetAllSemestersByUniversityId(string chosenUniversityId)
        {
            return TransferDataFromEntityToModel().Where(x => x.UniversityId == chosenUniversityId).OrderBy(x => x.Number);
        }

        public void DeleteSemesterById(string semesterId)
        {
            _semesterRepository.DeleteSemester(semesterId);
        }
    }
}
