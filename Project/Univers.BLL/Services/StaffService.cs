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
    public class StaffService
    {
        private readonly StaffRepository _staffRepository;
        private readonly Univers.Utilities.Utilities _utilities;

        public StaffService()
        {
            _staffRepository = new StaffRepository();
            _utilities = new Univers.Utilities.Utilities();
        }

        /// <summary>
        /// Transfer data from Staff entity to Staff model
        /// </summary>
        /// <returns></returns>
        public List<StaffModel> TransferDataFromEntityToModel()
        {
            List<StaffModel> models = new();

            List<Staff> entities = _staffRepository.ReadAllData();

            foreach (var entity in entities)
            {
                var newModel = new StaffModel();

                newModel.Id = entity.Id;
                newModel.UserId = entity.UserId; 
                newModel.Role = entity.Role;

                models.Add(newModel);
            }

            return models;
        }

        public StaffModel? GetStaffByUserId(string userId)
        {
            List<StaffModel> staff = TransferDataFromEntityToModel();

            return staff.FirstOrDefault(x => x.UserId == userId);
        }
    }
}
