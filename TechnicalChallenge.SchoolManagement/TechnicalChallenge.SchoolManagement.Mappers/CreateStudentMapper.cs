using TechnicalChallenge.SchoolManagement.Dto.Student;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Mappers
{
    public class CreateStudentMapper : IMapper<CreateStudentRequestDto, Student>
    {
        public Student ToEntity(CreateStudentRequestDto dtoInput)
        {
            return new Student
            {
                Name = dtoInput.Name,
                LastName = dtoInput.LastName,
                GenderId = dtoInput.GenderId,
                BirthDate = dtoInput.BirthDate
            };
        }
    }
}
