using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univers.DAL.Entities;

namespace Univers.DAL.Repositories
{
    public class SubjectSemesterRepository
    {
        // Read the data from the SubjectSemester table
        public static List<SubjectSemester> ReadAllData()
        {
            using Context.Context context = new();

            return context.SubjectSemesters.ToList();
        }
    }
}
