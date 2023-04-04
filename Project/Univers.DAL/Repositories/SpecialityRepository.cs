using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class SpecialityRepository
    {
        /// <summary>
        /// Read the data from the Speciality table
        /// </summary>
        /// <returns></returns>
        public List<Speciality> ReadAllData()
        {
            using Context.Context context = new();

            return context.Specialities.ToList();
        }
    }
}
