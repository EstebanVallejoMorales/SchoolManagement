using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Models
{
    public class TeacherModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        // Gender relationship
        public int GenderId { get; set; }
        public GenderModel Gender { get; set; }

        // Grades relationship
        public ICollection<GradeModel> Grades { get; set; } // A teacher can be assigned to different Grades.
                                                            
        public ICollection<TeacherAssignmentModel> TeacherAssignments { get; set; }
    }
}
