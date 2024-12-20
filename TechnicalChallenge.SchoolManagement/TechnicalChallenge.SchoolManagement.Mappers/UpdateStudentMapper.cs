using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.Student;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Mappers
{
    public class UpdateStudentMapper : IMapper<UpdateStudentRequestDto, Student>
    {
        public Student ToEntity(UpdateStudentRequestDto dtoInput)
        {
            return new Student
            {
                Id = dtoInput.Id,
                Name = dtoInput.Name,
                LastName = dtoInput.LastName,
                GenderId = dtoInput.GenderId
            };
        }
    }
}
