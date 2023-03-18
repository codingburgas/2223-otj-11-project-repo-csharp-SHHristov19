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
        // Transfer data from Subject entity to Subject model
        public static List<Subject> TransferDataFromEntityToModel()
        {
            List<Subject> models = new();

            var entities = SubjectRepository.ReadAllData();

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
