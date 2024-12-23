using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GradeGroup;
using TechnicalChallenge.SchoolManagement.Dto.Group;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Mappers
{
    public class UpdateGradeGroupMapper : IMapper<UpdateGradeGroupRequestDto, GradeGroup>
    {
        public GradeGroup ToEntity(UpdateGradeGroupRequestDto dtoInput)
        {
            return new GradeGroup
            {
                Id = dtoInput.Id,
                GradeId = dtoInput.GradeId,
                GroupId = dtoInput.GroupId
            };
        }
    }
}
