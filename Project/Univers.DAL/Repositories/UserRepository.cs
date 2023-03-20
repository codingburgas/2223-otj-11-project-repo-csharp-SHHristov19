using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class UserRepository
    {
        /// <summary>
        /// Read the data from the Users table
        /// </summary>
        /// <returns></returns>
        public List<User> ReadAllData()
        {
            using Context.Context context = new();

            return context.Users.ToList();
        }
    }
}
