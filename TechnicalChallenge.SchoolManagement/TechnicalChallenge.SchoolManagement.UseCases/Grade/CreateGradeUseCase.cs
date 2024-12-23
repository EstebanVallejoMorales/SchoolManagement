using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Grade
{
    public class CreateGradeUseCase<TDtoInput>
    {
        private readonly IRepository<Entities.Grade> _gradeRepository;
        private readonly IMapper<TDtoInput, Entities.Grade> _mapper;

        public CreateGradeUseCase(IRepository<Entities.Grade> gradeRepository, IMapper<TDtoInput, Entities.Grade> mapper)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(TDtoInput createGradeRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                Entities.Grade grade = _mapper.ToEntity(createGradeRequestDto);

                int responseInt = await _gradeRepository.AddAsync(grade);
                if (responseInt == 0)
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo crear el grado." });
                }
                else
                {
                    responseDto.Message = "Grado creado exitosamente";
                    responseDto.Data = responseInt;
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo crear el grado." });
            }
            return responseDto;
        }
    }
}
