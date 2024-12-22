using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.Teacher;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Mappers
{
    public class UpdateTeacherMapper: IMapper<UpdateTeacherRequestDto, Teacher>
    {
        public Teacher ToEntity(UpdateTeacherRequestDto dtoInput)
        {
            return new Teacher
            {
                Id = dtoInput.Id,
                Name = dtoInput.Name,
                LastName = dtoInput.LastName,
                GenderId = dtoInput.GenderId
            };
        }
    }
}
