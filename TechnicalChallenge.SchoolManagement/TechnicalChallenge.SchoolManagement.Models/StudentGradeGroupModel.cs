using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Models
{
    public class StudentGradeGroupModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public StudentModel Student { get; set; }
        public int GradeGroupId { get; set; }
        public GradeGroupModel GradeGroup { get; set; }
    }
}
