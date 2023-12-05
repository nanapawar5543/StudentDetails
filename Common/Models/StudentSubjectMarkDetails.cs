using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class StudentSubjectMarkDetails
    {
        public int? Rollno { get; set; }
        public List<SubjectMaster>? ListSubjectMasters { get; set; }
        public List<StudentMarks>? ListStudentMarks { get; set; }
    }
}
