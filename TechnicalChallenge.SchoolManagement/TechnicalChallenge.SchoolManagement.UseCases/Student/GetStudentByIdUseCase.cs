using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class GetStudentByIdUseCase<T>
    {
        private readonly IRepository<T> _studentRepository;

        public GetStudentByIdUseCase(IRepository<T> studentRepository)
        {
            _studentRepository = studentRepository;            
        }

        public async Task<T?> ExecuteAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }
    }
}
