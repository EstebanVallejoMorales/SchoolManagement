using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Models
{
    public class GradeGroupModel
    {
        public int Id { get; set; }
        public int GradeId { get; set; }
        public GradeModel Grade { get; set; }
        public int GroupId { get; set; }
        public GroupModel Group { get; set; }

        // Relationship: StudentGradeGroup
        public ICollection<StudentGradeGroupModel> StudentGradeGroups { get; set; }
        public ICollection<TeacherGradeGroupOwnershipModel> TeacherGradeGroupOwnerships { get; set; }
        public ICollection<TeacherGradeGroupClassAssignmentModel> TeacherGradeGroupClassAssignments { get; set; }
    }
}
