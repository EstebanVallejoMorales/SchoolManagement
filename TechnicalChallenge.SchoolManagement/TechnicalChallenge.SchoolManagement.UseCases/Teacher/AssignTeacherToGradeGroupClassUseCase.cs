using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Teacher
{
    public class AssignTeacherToGradeGroupClassUseCase<TDtoInput>
    {
        private readonly IRepository<Entities.TeacherGradeGroupClassAssignment> _gradeStudentGradeGroupRepository;
        private readonly IMapper<TDtoInput, Entities.TeacherGradeGroupClassAssignment> _mapper;

        public AssignTeacherToGradeGroupClassUseCase(IRepository<Entities.TeacherGradeGroupClassAssignment> gradeStudentGradeGroupRepository,
            IMapper<TDtoInput, Entities.TeacherGradeGroupClassAssignment> mapper)
        {
            _gradeStudentGradeGroupRepository = gradeStudentGradeGroupRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(TDtoInput assignStudentToGradeGroupDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                Entities.TeacherGradeGroupClassAssignment teacherGradeGroupClassAssignment = _mapper.ToEntity(assignStudentToGradeGroupDto);

                int responseInt = await _gradeStudentGradeGroupRepository.AddAsync(teacherGradeGroupClassAssignment);
                if (responseInt == 0)
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo asignar el profesor a un Grado-Grupo." });
                }
                else
                {
                    responseDto.Message = "Asignación de profesor a un Grado-Grupo exitosa.";
                    responseDto.Data = responseInt;
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo asignar el estudiante a un Grado-Grupo." });
            }
            return responseDto;
        }
    }
}
