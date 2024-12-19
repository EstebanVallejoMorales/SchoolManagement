using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class AssignStudentToGradeGroup
    {
        private readonly IRepository<Entities.Student> _studentRepository;

        public AssignStudentToGradeGroup(IRepository<Entities.Student> studentRepository)
        {
            _studentRepository = studentRepository;            
        }
    }
}
