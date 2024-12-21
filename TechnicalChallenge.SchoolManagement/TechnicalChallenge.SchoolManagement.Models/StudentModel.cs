using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public GenderModel Gender { get; set; }

        // Relationship: StudentGradeGroup
        public ICollection<StudentGradeGroupModel> StudentGradeGroups { get; set; }
    }
}
