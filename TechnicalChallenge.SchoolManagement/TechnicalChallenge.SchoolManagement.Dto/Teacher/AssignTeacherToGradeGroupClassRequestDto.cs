using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Dto.Teacher
{
    public class AssignTeacherToGradeGroupClassRequestDto
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int GradeGroupId { get; set; }
    }
}
