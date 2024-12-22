using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.Student;
using TechnicalChallenge.SchoolManagement.Dto.Teacher;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Mappers
{
    public class CreateTeacherMapper: IMapper<CreateTeacherRequestDto, Teacher>
    {
        public Teacher ToEntity(CreateTeacherRequestDto dtoInput)
        {
            return new Teacher
            {
                Name = dtoInput.Name,
                LastName = dtoInput.LastName,
                GenderId = dtoInput.GenderId,
            };
        }
    }
}
