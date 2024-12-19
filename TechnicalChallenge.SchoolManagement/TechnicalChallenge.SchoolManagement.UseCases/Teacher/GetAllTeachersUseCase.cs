﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Teacher

{
    public class GetAllTeachersUseCase
    {
        private readonly IRepository<Entities.Teacher> _teacherRepository;

        public GetAllTeachersUseCase(IRepository<Entities.Teacher> teacherRepository)
        {
            _teacherRepository = teacherRepository;            
        }
    }
}
