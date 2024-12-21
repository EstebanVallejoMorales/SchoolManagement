using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.Dto.Student;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class AddStudentToGradeGroupUseCase<TDtoInput>
    {
        private readonly IRepository<Entities.StudentGradeGroup> _gradeStudentGradeGroupRepository;
        private readonly IMapper<TDtoInput, Entities.StudentGradeGroup> _mapper;

        public AddStudentToGradeGroupUseCase(IRepository<Entities.StudentGradeGroup> gradeStudentGradeGroupRepository,
            IMapper<TDtoInput, Entities.StudentGradeGroup> mapper)
        {
            _gradeStudentGradeGroupRepository = gradeStudentGradeGroupRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(TDtoInput assignStudentToGradeGroupDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                Entities.StudentGradeGroup student = _mapper.ToEntity(assignStudentToGradeGroupDto);

                int responseInt = await _gradeStudentGradeGroupRepository.AddAsync(student);
                if (responseInt == 0)
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo asignar el estudiante a un grupo." });
                }
                else
                {
                    responseDto.Message = "Asignación de estudiante a grupo exitosa.";
                    responseDto.Data = responseInt;
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo asignar el estudiante a un grupo." });
            }
            return responseDto;
        }
    }
}
