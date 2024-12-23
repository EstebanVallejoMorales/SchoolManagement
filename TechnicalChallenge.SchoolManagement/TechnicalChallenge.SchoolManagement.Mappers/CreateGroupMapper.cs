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
    public class CreateGroupMapper : IMapper<CreateGroupRequestDto, Group>
    {
        public Group ToEntity(CreateGroupRequestDto dtoInput)
        {
            return new Group
            {
                Name = dtoInput.Name
            };
        }
    }
}
