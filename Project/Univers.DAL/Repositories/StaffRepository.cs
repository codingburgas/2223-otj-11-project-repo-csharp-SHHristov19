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
    }
}
