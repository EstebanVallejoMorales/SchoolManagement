﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class GetAllStudentUseCase<TInputEntity, TOutput>
    {
        private readonly IRepository<TInputEntity> _studentRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetAllStudentUseCase(IRepository<TInputEntity> studentRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _studentRepository = studentRepository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
        {
            var students = await _studentRepository.GetAllAsync();
            return _presenter.Present(students);
        }
    }
}
