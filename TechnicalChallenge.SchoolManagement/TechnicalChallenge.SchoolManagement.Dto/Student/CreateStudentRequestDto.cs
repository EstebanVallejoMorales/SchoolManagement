using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Dto.Student
{
    public class CreateStudentRequestDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
