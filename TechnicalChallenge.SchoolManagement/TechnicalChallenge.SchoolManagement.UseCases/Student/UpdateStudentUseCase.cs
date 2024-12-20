using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class UpdateStudentUseCase<TDtoInput>
    {
        private readonly IRepository<Entities.Student> _studentRepository;
        private readonly IMapper<TDtoInput, Entities.Student> _mapper;

        public UpdateStudentUseCase(IRepository<Entities.Student> studentRepository, IMapper<TDtoInput, Entities.Student> mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(TDtoInput updateStudentRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                Entities.Student student = _mapper.ToEntity(updateStudentRequestDto);

                int responseInt = await _studentRepository.UpdateAsync(student);
                if (responseInt == 0)
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo actualizar el estudiante." });
                }
                else
                {
                    responseDto.Message = "Estudiante actualizado exitosamente";
                    responseDto.Data = responseInt;
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo actualizar el estudiante." });
            }
            return responseDto;
        }
    }
}
