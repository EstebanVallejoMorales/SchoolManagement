using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class DeleteStudentUseCase
    {
        private readonly IRepository<Entities.Student> _studentRepository;

        public DeleteStudentUseCase(IRepository<Entities.Student> studentRepository)
        {
            _studentRepository = studentRepository;            
        }
    }
}
