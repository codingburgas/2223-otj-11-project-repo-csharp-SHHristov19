using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class StaffRepository
    { 
        /// <summary>
        /// Read the data from the Staff table
        /// </summary>
        /// <returns></returns>
        public List<Staff> ReadAllData()
        {
            using Context.Context context = new();

            return context.Staff.ToList();
        }

        public void UpdateStaffRoleByUserId(string userId, string newRole)
        {
            using Context.Context context = new();

            var staff = context.Staff.FirstOrDefault(x => x.UserId == userId);

            staff.Role = newRole;

            context.Staff.Update(staff);
            context.SaveChanges();
        }

        public void AddStaffByUserId(string userId, string newRole)
        {
            using Context.Context context = new();

            var staff = new Staff();

            staff.Id = Guid.NewGuid().ToString("D");
            staff.UserId = userId;
            staff.Role = newRole;

            context.Staff.Add(staff);
            context.SaveChanges();
        } 
    }
}
