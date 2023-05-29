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
                StaffModel newModel = MapStaffEntity(entity);

                models.Add(newModel);
            }

            return models;
        }

        /// <summary>
        /// Maps a Staff entity to a StaffModel.
        /// </summary>
        /// <param name="entity">The Staff entity to be mapped.</param>
        /// <returns>A StaffModel representing the mapped entity.</returns>
        public StaffModel MapStaffEntity(Staff entity)
        {
            var newModel = new StaffModel();

            newModel.Id = entity.Id;
            newModel.UserId = entity.UserId;
            newModel.Role = entity.Role;
            return newModel;
        }

        /// <summary>
        /// Retrieves a StaffModel for a specific user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A StaffModel representing the staff, or null if not found.</returns>
        public StaffModel? GetStaffByUserId(string userId)
        {
            List<StaffModel> staff = TransferDataFromEntityToModel();

            return staff.FirstOrDefault(x => x.UserId == userId);
        }

        /// <summary>
        /// Adds a staff with the specified user ID and role.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="newRole">The role of the staff to be added.</param>
        public void AddStaffByUserId(string userId, string newRole)
        {
            _staffRepository.AddStaffByUserId(userId, newRole);
        }

        /// <summary>
        /// Updates the role of a staff with the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="newRole">The new role to be updated.</param>
        public void UpdateStaffRoleByUserId(string userId, string newRole)
        {
            _staffRepository.UpdateStaffRoleByUserId(userId, newRole);
        }

        /// <summary>
        /// Retrieves a StaffModel for a specific staff ID.
        /// </summary>
        /// <param name="staffId">The ID of the staff.</param>
        /// <returns>A StaffModel representing the staff, or null if not found.</returns>
        public StaffModel? GetStaffById(string? staffId)
        {
            return TransferDataFromEntityToModel().FirstOrDefault(x => x.Id == staffId);
        }
    }
}
