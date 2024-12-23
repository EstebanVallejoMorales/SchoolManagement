using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.Grade;
using TechnicalChallenge.SchoolManagement.Dto.GradeGroup;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Mappers
{
    public class CreateGradeGroupMapper : IMapper<CreateGradeGroupRequestDto, GradeGroup>
    {
        public GradeGroup ToEntity(CreateGradeGroupRequestDto dtoInput)
        {
            return new GradeGroup
            {
                GradeId = dtoInput.GradeId,
                GroupId = dtoInput.GroupId
            };
        }
    }
}
