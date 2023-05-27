using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.Models.Models
{
    public class TeacherModel
    {
        public string? UserId { get; set; }

        public List<StudentGradesModel>? Students { get; set; }

        public List<SubjectModel>? Subjects { get; set; }

        public SubjectModel? ChoosenSubject { get; set; }

        public StudentGradesModel? EditGrade { get; set; }
    }
}
