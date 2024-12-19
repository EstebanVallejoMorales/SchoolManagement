using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Models
{
    public class GroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // StudentGradeModel relationship
        public ICollection<StudentGradeModel> StudentGrades { get; set; } // A Group can be related with 9th Grade, but also with 10th grade

        // TeacherAssignment relationship
        public ICollection<TeacherAssignmentModel> TeacherAssignments { get; set; } // A Group can be related with teacher X, but also with Teacher Y

    }
}
