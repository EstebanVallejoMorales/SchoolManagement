using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.GradeGroup
{
    public class CreateGradeGroupUseCase<TDtoInput>
    {
        private readonly IRepository<Entities.GradeGroup> _gradeGroupRepository;
        private readonly IMapper<TDtoInput, Entities.GradeGroup> _mapper;

        public CreateGradeGroupUseCase(IRepository<Entities.GradeGroup> gradeGroupRepository, IMapper<TDtoInput, Entities.GradeGroup> mapper)
        {
            _gradeGroupRepository = gradeGroupRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(TDtoInput createGradeGroupRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                Entities.GradeGroup gradeGroup = _mapper.ToEntity(createGradeGroupRequestDto);

                int responseInt = await _gradeGroupRepository.AddAsync(gradeGroup);
                if (responseInt == 0)
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo crear el Grado-Grupo." });
                }
                else
                {
                    responseDto.Message = "Grado-Grupo creado exitosamente";
                    responseDto.Data = responseInt;
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo crear el Grado-Grupo." });
            }
            return responseDto;
        }
    }
}
