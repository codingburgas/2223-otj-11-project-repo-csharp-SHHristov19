using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class StudentRepository
    {
        /// <summary>
        /// Read the data from the Student table
        /// </summary>
        /// <returns></returns>
        public List<Student> ReadAllData()
        {
            using Context.Context context = new();

            return context.Students.ToList();
        }
    }
}
