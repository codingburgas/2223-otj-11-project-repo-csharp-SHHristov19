using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class FacultySpecialityRepository
    {
        /// <summary>
        /// Read the data from the FacultySpeciality table
        /// </summary>
        /// <returns></returns>
        public List<FacultySpeciality> ReadAllData()
        {
            using Context.Context context = new();

            return context.FacultySpecialities.ToList();
        }
    }
}
