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
    public class AssignTeacherToGradeGroupOwnershipMapper : IMapper<AssignTeacherToGradeGroupOwnershipRequestDto, TeacherGradeGroupOwnership>
    {
        public TeacherGradeGroupOwnership ToEntity(AssignTeacherToGradeGroupOwnershipRequestDto dtoInput)
        {
            return new TeacherGradeGroupOwnership
            {
                TeacherId = dtoInput.TeacherId,
                GradeGroupId = dtoInput.GradeGroupId
            };
        }
    }
}
