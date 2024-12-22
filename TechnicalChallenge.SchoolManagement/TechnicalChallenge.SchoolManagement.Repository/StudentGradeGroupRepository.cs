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
    public class StudentGradeGroupRepository : IRepository<StudentGradeGroup>
    {
        private readonly AppDbContext _dbContext;

        public StudentGradeGroupRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(StudentGradeGroup studentGradeGroup)
        {
            int createdElements;
            var gradeGroupModel = new StudentGradeGroupModel
            {
                StudentId = studentGradeGroup.StudentId,
                GradeGroupId = studentGradeGroup.GradeGroupId
            };

            await _dbContext.AddAsync(gradeGroupModel);
            createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> DeleteAsync(int studentGradeGroupId)
        {
            int removedElements;
            var gradeGroup = await _dbContext.StudentGradeGroups.FindAsync(studentGradeGroupId);

            if (gradeGroup == null)
            {
                throw new Exception("Asignación de estudiante a Grado-Grupo no encontrada.");
            }

            _dbContext.StudentGradeGroups.Remove(gradeGroup);

            removedElements = await _dbContext.SaveChangesAsync();

            return removedElements;
        }

        public async Task<IEnumerable<StudentGradeGroup>> GetAllAsync()
        {
            return await _dbContext.StudentGradeGroups.Select(sg => new StudentGradeGroup
            {
                Id = sg.Id,
                StudentId = sg.StudentId,
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

        public async Task<StudentGradeGroup?> GetByIdAsync(int id)
        {
            var studentGradeGroupModel = await _dbContext.StudentGradeGroups.FindAsync(id);
            if (studentGradeGroupModel != null)
            {
                return new StudentGradeGroup
                {
                    Id = studentGradeGroupModel.Id,
                    StudentId = studentGradeGroupModel.StudentId,
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

        public async Task<int> UpdateAsync(StudentGradeGroup studentGradeGroup)
        {
            int updatedElements;
            var studentGradeGroupFound = await _dbContext.StudentGradeGroups.FindAsync(studentGradeGroup.Id);

            if (studentGradeGroupFound == null)
            {
                throw new Exception("Asignación de estudiante a Grado-Grupo no encontrada.");
            }

            studentGradeGroupFound.StudentId = studentGradeGroup.StudentId;
            studentGradeGroupFound.GradeGroupId = studentGradeGroup.GradeGroupId;

            updatedElements = await _dbContext.SaveChangesAsync();
            return updatedElements;
        }
    }
}
