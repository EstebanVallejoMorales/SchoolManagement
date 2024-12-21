using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Models
{
    public class TeacherGradeGroupClassAssignmentModel
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public TeacherModel Teacher { get; set; }
        public int GradeGroupId { get; set; }
        public GradeGroupModel GradeGroup { get; set; }
    }
}
