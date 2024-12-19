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
    public class CreateStudentUseCase
    {
        private readonly IRepository<Entities.Student> _studentRepository;

        public CreateStudentUseCase(IRepository<Entities.Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(CreateStudentRequestDto createStudentRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();

            var student = new Entities.Student 
            {
                GenderId = createStudentRequestDto.GenderId,
                Name = createStudentRequestDto.Name,
                LastName = createStudentRequestDto.LastName
            };
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
            return responseDto;
        }
    }
}
