using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Teacher
{
    public class CreateTeacherUseCase<TDtoInput>
    {
        private readonly IRepository<Entities.Teacher> _teacherRepository;
        private readonly IMapper<TDtoInput, Entities.Teacher> _mapper;

        public CreateTeacherUseCase(IRepository<Entities.Teacher> teacherRepository, IMapper<TDtoInput, Entities.Teacher> mapper)
        {
            _teacherRepository = teacherRepository;            
            _mapper = mapper;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(TDtoInput createTeacherRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                Entities.Teacher Teacher = _mapper.ToEntity(createTeacherRequestDto);

                int responseInt = await _teacherRepository.AddAsync(Teacher);
                if (responseInt == 0)
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo crear el profesor." });
                }
                else
                {
                    responseDto.Message = "Profesor creado exitosamente";
                    responseDto.Data = responseInt;
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo crear el profesor." });
            }
            return responseDto;
        }
    }
}
