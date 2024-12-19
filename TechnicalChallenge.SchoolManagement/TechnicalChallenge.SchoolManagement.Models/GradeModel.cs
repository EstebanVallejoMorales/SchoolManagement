using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Models
{
    public class GradeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Teacher relationship
        public int TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }

        // StudentGradeModel relationship
        public ICollection<StudentGradeModel> StudentGrades { get; set; } // A Grade has several students but it needs also the Group information

        // TeacherAssignment relationship
        public ICollection<TeacherAssignmentModel> TeacherAssignments { get; set; } // A Grade can be related with several Groups and Teachers in TeacherAssignment
    }
}
