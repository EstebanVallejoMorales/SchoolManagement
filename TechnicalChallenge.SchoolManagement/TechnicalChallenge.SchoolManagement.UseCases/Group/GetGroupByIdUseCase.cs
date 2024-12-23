using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Group
{
    public class GetGroupByIdUseCase<TInputEntity, TOutput> where TInputEntity : class
    {
        private readonly IRepository<TInputEntity> _groupRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetGroupByIdUseCase(IRepository<TInputEntity> groupRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _groupRepository = groupRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<TOutput?>> ExecuteAsync(int id)
        {
            ResponseDto<TOutput?> responseDto = new ResponseDto<TOutput?>();
            try
            {
                var group = await _groupRepository.GetByIdAsync(id);
                if (group != null)
                {
                    var groupViewModel = _presenter.Present(group);
                    responseDto.Data = groupViewModel;
                }
                else
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
