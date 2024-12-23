using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.Grade;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Mappers
{
    public class UpdateGradeMapper : IMapper<UpdateGradeRequestDto, Grade>
    {
        public Grade ToEntity(UpdateGradeRequestDto dtoInput)
        {
            return new Grade
            {
                Id = dtoInput.Id,
                Name = dtoInput.Name
            };
        }
    }
}
