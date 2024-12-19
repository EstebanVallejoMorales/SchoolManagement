using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Models
{
    public class TeacherAssignmentModel
    {
        public int Id { get; set; }

        // Teacher relationship
        public int TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }

        // Grade relationship
        public int GradeId { get; set; }
        public GradeModel Grade { get; set; }

        // Group relationship
        public int GroupId { get; set; }
        public GroupModel Group { get; set; }
    }
}
