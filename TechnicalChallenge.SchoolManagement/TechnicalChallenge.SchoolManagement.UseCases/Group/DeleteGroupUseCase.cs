using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Group
{
    public class DeleteGroupUseCase<TInputEntity>
    {
        private readonly IRepository<TInputEntity> _groupRepository;

        public DeleteGroupUseCase(IRepository<TInputEntity> groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(int id)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                int result = await _groupRepository.DeleteAsync(id);
                responseDto.Data = result;
                if (result != 0)
                {
                    responseDto.Message = $"Grupo eliminado exitosamente";
                }
                if (result != 0)
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = $"No se encontró el grupo con id {id}." });
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener el grupo con id {id}"
                });
            }
            return responseDto;
        }
    }
}
