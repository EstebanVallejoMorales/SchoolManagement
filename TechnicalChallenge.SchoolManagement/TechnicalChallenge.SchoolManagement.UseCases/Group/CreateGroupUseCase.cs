using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Group
{
    public class CreateGroupUseCase<TDtoInput>
    {
        private readonly IRepository<Entities.Group> _groupRepository;
        private readonly IMapper<TDtoInput, Entities.Group> _mapper;

        public CreateGroupUseCase(IRepository<Entities.Group> groupRepository, IMapper<TDtoInput, Entities.Group> mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(TDtoInput createGroupRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                Entities.Group group = _mapper.ToEntity(createGroupRequestDto);

                int responseInt = await _groupRepository.AddAsync(group);
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
