using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Teacher
{
    public class UpdateTeacherUseCase<TDtoInput>
    {
        private readonly IRepository<Entities.Teacher> _teacherRepository;
        private readonly IMapper<TDtoInput, Entities.Teacher> _mapper;

        public UpdateTeacherUseCase(IRepository<Entities.Teacher> teacherRepository, IMapper<TDtoInput, Entities.Teacher> mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(TDtoInput updateTeacherRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                Entities.Teacher teacher = _mapper.ToEntity(updateTeacherRequestDto);

                int responseInt = await _teacherRepository.UpdateAsync(teacher);
                if (responseInt == 0)
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo actualizar el profesor." });
                }
                else
                {
                    responseDto.Message = "Estudiante actualizado exitosamente";
                    responseDto.Data = responseInt;
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo actualizar el profesor." });
            }
            return responseDto;
        }
    }
}
