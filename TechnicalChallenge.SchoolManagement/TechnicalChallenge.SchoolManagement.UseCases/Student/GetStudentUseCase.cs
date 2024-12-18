using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;
using TechnicalChallenge.SchoolManagement.Entities;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class GetStudentUseCase
    {
        private readonly IRepository<Entities.Student> _studentRepository;

        public GetStudentUseCase(IRepository<Entities.Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<Entities.Student>> ExecuteAsync()
        {
            return await _studentRepository.GetAllAsync();
        }
    }
}
