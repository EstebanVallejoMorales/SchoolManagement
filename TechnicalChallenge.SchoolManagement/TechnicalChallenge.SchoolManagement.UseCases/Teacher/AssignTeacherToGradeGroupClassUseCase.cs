﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Dto.GenericResponse;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.UseCases.Teacher
{
    public class AssignTeacherToGradeGroupClassUseCase<TDtoInput>
    {
        private readonly IRepository<Entities.TeacherGradeGroupClassAssignment> _teacherGradeGroupClassAssignmentRespository;
        private readonly IMapper<TDtoInput, Entities.TeacherGradeGroupClassAssignment> _mapper;

        public AssignTeacherToGradeGroupClassUseCase(IRepository<Entities.TeacherGradeGroupClassAssignment> teacherGradeGroupClassAssignmentRespository,
            IMapper<TDtoInput, Entities.TeacherGradeGroupClassAssignment> mapper)
        {
            _teacherGradeGroupClassAssignmentRespository = teacherGradeGroupClassAssignmentRespository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<int>> ExecuteAsync(TDtoInput assignTeacherClassRequestDto)
        {
            ResponseDto<int> responseDto = new ResponseDto<int>();
            try
            {
                Entities.TeacherGradeGroupClassAssignment teacherGradeGroupClassAssignment = _mapper.ToEntity(assignTeacherClassRequestDto);

                int responseInt = await _teacherGradeGroupClassAssignmentRespository.AddAsync(teacherGradeGroupClassAssignment);
                if (responseInt == 0)
                {
                    responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo asignar el profesor a un Grado-Grupo." });
                }
                else
                {
                    responseDto.Message = "Asignación de profesor a un Grado-Grupo exitosa.";
                    responseDto.Data = responseInt;
                }
            }
            catch (Exception ex)
            {
                responseDto.Errors.Add(new Dto.Error.ErrorDto { Message = "No se pudo asignar el estudiante a un Grado-Grupo." });
            }
            return responseDto;
        }
    }
}
