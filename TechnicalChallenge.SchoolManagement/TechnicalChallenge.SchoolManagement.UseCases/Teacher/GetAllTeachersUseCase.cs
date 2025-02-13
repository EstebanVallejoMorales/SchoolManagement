﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Teacher

{
    public class GetAllTeachersUseCase<TInputEntity, TOutput>
    {
        private readonly IRepository<TInputEntity> _teacherRepository;
        private readonly IPresenter<TInputEntity, TOutput> _presenter;

        public GetAllTeachersUseCase(IRepository<TInputEntity> teacherRepository, IPresenter<TInputEntity, TOutput> presenter)
        {
            _teacherRepository = teacherRepository;
            _presenter = presenter;
        }

        public async Task<ResponseDto<IEnumerable<TOutput>>> ExecuteAsync()
        {
            ResponseDto<IEnumerable<TOutput>> responseDto = new ResponseDto<IEnumerable<TOutput>>();
            try
            {
                var teachers = await _teacherRepository.GetAllAsync();

                var teachersViewModel = _presenter.Present(teachers);
                responseDto.Data = teachersViewModel;
                if (!responseDto.Data.Any())
                {
                    responseDto.Message = "No se encontraron profesores.";
                }
                else
                {
                    responseDto.Message = "Profesores cargados exitosamente.";
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto
                {
                    Message = $"Ocurrió un error al tratar de obtener los profesores."
                });
            }
            return responseDto;
        }
    }
}
