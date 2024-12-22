using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Data;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Models;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Repository
{
    public class TeacherGradeGroupClassAssignmentRespository : IRepository<TeacherGradeGroupClassAssignment>
    {
        private readonly AppDbContext _dbContext;

        public TeacherGradeGroupClassAssignmentRespository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(TeacherGradeGroupClassAssignment studentGradeGroup)
        {
            int createdElements;
            var gradeGroupModel = new TeacherGradeGroupClassAssignmentModel
            {
                TeacherId = studentGradeGroup.TeacherId,
                GradeGroupId = studentGradeGroup.GradeGroupId
            };

            await _dbContext.AddAsync(gradeGroupModel);
            createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> DeleteAsync(int studentGradeGroupId)
        {
            int removedElements;
            var gradeGroup = await _dbContext.TeacherGradeGroupClassAssignments.FindAsync(studentGradeGroupId);

            if (gradeGroup == null)
            {
                throw new Exception("Asignación de profesor a Grado-Grupo no encontrada.");
            }

            _dbContext.TeacherGradeGroupClassAssignments.Remove(gradeGroup);

            removedElements = await _dbContext.SaveChangesAsync();

            return removedElements;
        }

        public async Task<IEnumerable<TeacherGradeGroupClassAssignment>> GetAllAsync()
        {
            return await _dbContext.TeacherGradeGroupClassAssignments.Select(sg => new TeacherGradeGroupClassAssignment
            {
                Id = sg.Id,
                TeacherId = sg.TeacherId,
                GradeGroupId = sg.GradeGroupId,
                GradeGroup = new GradeGroup
                {
                    Grade = new Grade
                    {
                        Name = sg.GradeGroup.Grade.Name,
                        Id = sg.GradeGroup.Grade.Id
                    },
                    Group = new Group
                    {
                        Id = sg.GradeGroup.Group.Id,
                        Name = sg.GradeGroup.Group.Name
                    }
                }
            }).ToListAsync();
        }

        public async Task<TeacherGradeGroupClassAssignment?> GetByIdAsync(int id)
        {
            var studentGradeGroupModel = await _dbContext.TeacherGradeGroupClassAssignments.FindAsync(id);
            if (studentGradeGroupModel != null)
            {
                return new TeacherGradeGroupClassAssignment
                {
                    Id = studentGradeGroupModel.Id,
                    TeacherId = studentGradeGroupModel.TeacherId,
                    Teacher = new Teacher 
                    { 
                        Id = studentGradeGroupModel.Teacher.Id,
                        GenderId = studentGradeGroupModel.Teacher.GenderId,
                        Name = studentGradeGroupModel.Teacher.Name,
                        LastName = studentGradeGroupModel.Teacher.LastName
                    },
                    GradeGroupId = studentGradeGroupModel.GradeGroupId,
                    GradeGroup = new GradeGroup
                    {
                        Grade = new Grade
                        {
                            Name = studentGradeGroupModel.GradeGroup.Grade.Name,
                            Id = studentGradeGroupModel.GradeGroup.Grade.Id
                        },
                        Group = new Group
                        {
                            Id = studentGradeGroupModel.GradeGroup.Group.Id,
                            Name = studentGradeGroupModel.GradeGroup.Group.Name
                        }
                    }
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<int> UpdateAsync(TeacherGradeGroupClassAssignment studentGradeGroup)
        {
            int updatedElements;
            var studentGradeGroupFound = await _dbContext.TeacherGradeGroupClassAssignments.FindAsync(studentGradeGroup.Id);

            if (studentGradeGroupFound == null)
            {
                throw new Exception("Asignación de profesor a Grado-Grupo no encontrada.");
            }

            studentGradeGroupFound.TeacherId = studentGradeGroup.TeacherId;
            studentGradeGroupFound.GradeGroupId = studentGradeGroup.GradeGroupId;

            updatedElements = await _dbContext.SaveChangesAsync();
            return updatedElements;
        }
    }
}
