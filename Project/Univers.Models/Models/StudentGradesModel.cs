using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.Models.Models
{
    public class StudentGradesModel
    {
        public string? StudentId { get; set; }

        public UserModel? Student { get; set; }

        public List<GradeModel>? Grades { get; set; }
        
        public GradeModel? Grade { get; set; }
    }
}
