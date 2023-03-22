using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Repositories;
using Univers.Models.Models;

namespace Univers.BLL.Services
{
    public class SubjectService
    {
        private readonly SubjectRepository _subjectRepository;

        public SubjectService()
        {
            _subjectRepository = new SubjectRepository();
        }

        /// <summary>
        /// Transfer data from Subject entity to Subject model
        /// </summary>
        /// <returns></returns>
        public List<Subject> TransferDataFromEntityToModel()
        {
            List<Subject> models = new();

            var entities = _subjectRepository.ReadAllData();

            foreach (var entity in entities)
            {
                var newModel = new Subject();

                newModel.Id = entity.Id;
                newModel.TeacherId = entity.TeacherId;
                newModel.SpecialityId = entity.SpecialityId;
                newModel.Name = entity.Name;
                newModel.Type = entity.Type;
                newModel.Credits = (decimal)entity.Credits;
                newModel.List = entity.List;

                models.Add(newModel);
            }

            return models;
        }
    }
}
