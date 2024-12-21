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
    public class AddStudentToGradeGroupMapper : IMapper<AddStudentToGradeGroupRequestDto, StudentGradeGroup>
    {
        public StudentGradeGroup ToEntity(AddStudentToGradeGroupRequestDto dtoInput)
        {
            return new StudentGradeGroup
            {
                StudentId = dtoInput.StudentId,
                GradeGroupId = dtoInput.GradeGroupId
            };
        }
    }
}
