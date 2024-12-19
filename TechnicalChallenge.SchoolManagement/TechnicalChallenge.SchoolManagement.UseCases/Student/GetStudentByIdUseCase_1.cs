using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;
using TechnicalChallenge.SchoolManagement.Entities;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class GetStudentByIdUseCase<TInputEntity,TOutput>
    {
        private readonly IRepository<TInputEntity> _studentRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetStudentByIdUseCase(IRepository<TInputEntity> studentRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _studentRepository = studentRepository;
            _presenter = presenter;
        }

        public async Task<TOutput?> ExecuteAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            return _presenter.Present(student);
        }
    }
}
