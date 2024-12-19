using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;
using TechnicalChallenge.SchoolManagement.Entities;

namespace TechnicalChallenge.SchoolManagement.UseCases.Teacher
{
    public class GetTeacherUseCase
    {
        private readonly IRepository<Entities.Teacher> _teacherRepository;

        public GetTeacherUseCase(IRepository<Entities.Teacher> teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        public async Task<IEnumerable<Entities.Teacher>> ExecuteAsync()
        {
            return await _teacherRepository.GetAllAsync();
        }
    }
}
