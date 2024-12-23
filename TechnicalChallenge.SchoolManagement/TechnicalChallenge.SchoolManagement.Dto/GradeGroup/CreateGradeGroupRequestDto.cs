using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Dto.GradeGroup
{
    public class CreateGradeGroupRequestDto
    {
        public int Id { get; set; }
        public int GradeId { get; set; }
        public int GroupId { get; set; }
    }
}
