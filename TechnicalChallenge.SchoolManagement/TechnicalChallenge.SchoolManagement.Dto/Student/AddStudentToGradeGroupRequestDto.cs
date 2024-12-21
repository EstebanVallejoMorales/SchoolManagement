using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Dto.Student
{
    public class AddStudentToGradeGroupRequestDto
    {
        public int StudentId { get; set; }
        public int GradeGroupId { get; set; }
    }
}
