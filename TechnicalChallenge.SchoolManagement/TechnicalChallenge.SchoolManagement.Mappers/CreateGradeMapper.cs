using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.Grade;
using TechnicalChallenge.SchoolManagement.Dto.Student;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Mappers
{
    public class CreateGradeMapper : IMapper<CreateGradeRequestDto, Grade>
    {
        public Grade ToEntity(CreateGradeRequestDto dtoInput)
        {
            return new Grade
            {
                Name = dtoInput.Name
            };
        }
    }
}
