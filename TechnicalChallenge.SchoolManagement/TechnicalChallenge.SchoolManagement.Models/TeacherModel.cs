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
        public int GenderId { get; set; }
        public GenderModel Gender { get; set; }

        // Relationships
        public ICollection<TeacherGradeGroupOwnershipModel> TeacherGradeGroupOwnerships { get; set; }
        public ICollection<TeacherGradeGroupClassAssignmentModel> TeacherGradeGroupClassAssignments { get; set; }
    }
}
