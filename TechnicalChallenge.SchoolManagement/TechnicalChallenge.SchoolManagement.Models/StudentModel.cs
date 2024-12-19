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

        // Gender relationship
        public int GenderId { get; set; }
        public GenderModel Gender { get; set; }

        // StudentGrade relationship
        public StudentGradeModel StudentGrade { get; set; } // A Student only belongs to a single Grade
    }
}
