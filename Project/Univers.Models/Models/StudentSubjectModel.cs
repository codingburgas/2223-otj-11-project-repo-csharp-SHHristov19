using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Univers.Models.Models
{
    public class StudentSubjectModel
    {
        public string? StudentId { get; set; }

        public List<SubjectModel>? Subjects { get; set;}
    }
}
