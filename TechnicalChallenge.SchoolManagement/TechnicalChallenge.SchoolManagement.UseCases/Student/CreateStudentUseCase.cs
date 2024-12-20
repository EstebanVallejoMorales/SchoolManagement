using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.Dto.Student;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Student
{
    public class CreateStudentUseCase<TDtoInput>
    {
        private readonly IRepository<Entities.Student> _studentRepository;
        private readonly IMapper<TDtoInput, Entities.Student> _mapper;

        public CreateStudentUseCase(IRepository<Entities.Student> studentRepository, IMapper<TDtoInput, Entities.Student> mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(TDtoInput createStudentRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                Entities.Student student = _mapper.ToEntity(createStudentRequestDto);

                int responseInt = await _studentRepository.AddAsync(student);
                if (responseInt == 0)
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo crear el estudiante." });
                }
                else
                {
                    responseDto.Message = "Estudiante creado exitosamente";
                    responseDto.Data = responseInt;
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo crear el estudiante." });
            }
            return responseDto;
        }
    }
}
