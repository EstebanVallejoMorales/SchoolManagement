using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.Group;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Mappers
{
    public class UpdateGroupMapper : IMapper<UpdateGroupRequestDto, Group>
    {
        public Group ToEntity(UpdateGroupRequestDto dtoInput)
        {
            return new Group
            {
                Id = dtoInput.Id,
                Name = dtoInput.Name
            };
        }
    }
}
