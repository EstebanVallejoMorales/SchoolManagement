using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Entities
{
    public class TeacherAssignment
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int GradeId { get; set; }
        public int GroupId { get; set; }       
    }
}
