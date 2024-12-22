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
    public class AssignTeacherToGradeGroupClassMapper : IMapper<AssignTeacherToGradeGroupClassRequestDto, TeacherGradeGroupClassAssignment>
    {
        public TeacherGradeGroupClassAssignment ToEntity(AssignTeacherToGradeGroupClassRequestDto dtoInput)
        {
            return new TeacherGradeGroupClassAssignment
            {
                TeacherId = dtoInput.TeacherId,
                GradeGroupId = dtoInput.GradeGroupId
            };
        }
    }
}
