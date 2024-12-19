using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class UpdateStudentUseCase
    {
        private readonly IRepository<Entities.Student> _studentRepository;

        public UpdateStudentUseCase(IRepository<Entities.Student> studentRepository)
        {
            _studentRepository = studentRepository;            
        }
    }
}
