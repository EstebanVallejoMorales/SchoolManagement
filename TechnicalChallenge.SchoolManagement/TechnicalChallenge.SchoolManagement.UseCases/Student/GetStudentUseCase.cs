﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;
using TechnicalChallenge.SchoolManagement.Entities;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class GetStudentUseCase<T>
    {
        private readonly IRepository<T> _studentRepository;

        public GetStudentUseCase(IRepository<T> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IEnumerable<T>> ExecuteAsync()
        {
            return await _studentRepository.GetAllAsync();
        }
    }
}
