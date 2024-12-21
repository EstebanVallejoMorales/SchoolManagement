using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;

namespace TechnicalChallenge.SchoolManagement.UseCases.Interfaces
{
    public class GetAllGroupsUseCase<TInputEntity, TOutput>
    {
        private readonly IRepository<TInputEntity> _groupRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetAllGroupsUseCase(IRepository<TInputEntity> studentRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _groupRepository = studentRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<IEnumerable<TOutput>>> ExecuteAsync()
        {
            ResponseDto<IEnumerable<TOutput>> responseDto = new ResponseDto<IEnumerable<TOutput>>();
            try
            {
                var students = await _groupRepository.GetAllAsync();

                var studentsViewModel = _presenter.Present(students);
                responseDto.Data = studentsViewModel;
                if (!responseDto.Data.Any())
                {
                    responseDto.Message = "No se encontraron grupos.";
                }
                else
                {
                    responseDto.Message = "Grupos cargados exitosamente";
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener los grupos"
                });
            }
            return responseDto;
        }
    }
}
