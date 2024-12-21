using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Entities
{
    public class TeacherGradeGroupOwnership
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int GradeGroupId { get; set; }
        public GradeGroup GradeGroup { get; set; }
    }
}
