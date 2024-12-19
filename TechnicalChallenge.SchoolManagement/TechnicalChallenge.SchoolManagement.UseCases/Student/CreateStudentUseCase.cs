﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class CreateStudentUseCase
    {
        private readonly IRepository<Entities.Student> _studentRepository;

        public CreateStudentUseCase(IRepository<Entities.Student> studentRepository)
        {
            _studentRepository = studentRepository;            
        }

        public async Task<Entities.Student?> ExecuteAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);
        }
    }
}