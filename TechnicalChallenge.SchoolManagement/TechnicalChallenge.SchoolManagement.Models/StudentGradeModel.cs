using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Models
{
    public class StudentGradeModel
    {
        public int Id { get; set; }

        // Student relationship
        public int StudentId { get; set; }
        public StudentModel Student { get; set; }

        // Grade relationship
        public int GradeId { get; set; }
        public GradeModel Grade { get; set; }

        // Group relationship
        public int GroupId { get; set; }
        public GroupModel Group { get; set; }
    }
}
