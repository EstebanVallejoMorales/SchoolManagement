using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Teacher
{
    public class AssignTeacherToGradeGroupOwnershipUseCase<TDtoInput>
    {
        private readonly IRepository<Entities.TeacherGradeGroupOwnership> _teacherGradeGroupOwnershipRepository;
        private readonly IMapper<TDtoInput, Entities.TeacherGradeGroupOwnership> _mapper;

        public AssignTeacherToGradeGroupOwnershipUseCase(IRepository<Entities.TeacherGradeGroupOwnership> teacherGradeGroupOwnershipRepository,
            IMapper<TDtoInput, Entities.TeacherGradeGroupOwnership> mapper)
        {
            _teacherGradeGroupOwnershipRepository = teacherGradeGroupOwnershipRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(TDtoInput assignTeacherToGradeGroupOwnershipRequestRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                Entities.TeacherGradeGroupOwnership teacherGradeGroupClassAssignment = 
                    _mapper.ToEntity(assignTeacherToGradeGroupOwnershipRequestRequestDto);

                int responseInt = await _teacherGradeGroupOwnershipRepository.AddAsync(teacherGradeGroupClassAssignment);
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
