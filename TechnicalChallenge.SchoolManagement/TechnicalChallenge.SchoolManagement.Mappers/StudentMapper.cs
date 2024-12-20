using TechnicalChallenge.SchoolManagement.Dto.Student;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Mappers
{
    public class StudentMapper : IMapper<CreateStudentRequestDto, Student>
    {
        public Student ToEntity(CreateStudentRequestDto dtoInput)
        {
            return new Student
            {
                Name = dtoInput.Name,
                LastName = dtoInput.LastName,
                GenderId = dtoInput.GenderId
            };
        }
    }
}
