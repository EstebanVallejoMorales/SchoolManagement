using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Entities
{
    public class StudentGradeGroup
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int GradeGroupId { get; set; }
        public GradeGroup GradeGroup { get; set; }
    }
}
