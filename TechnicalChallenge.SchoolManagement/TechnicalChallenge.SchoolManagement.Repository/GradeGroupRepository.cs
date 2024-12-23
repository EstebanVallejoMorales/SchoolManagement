using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Data;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Models;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Repository
{
    public class GradeGroupRepository : IRepository<GradeGroup>
    {
        private readonly AppDbContext _dbContext;

        public GradeGroupRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(GradeGroup gradeGroup)
        {
            int createdElements;
            var gradeGroupModel = new GradeGroupModel
            {
               GradeId = gradeGroup.GradeId,
               GroupId = gradeGroup.GroupId,
            };

            await _dbContext.GradeGroups.AddAsync(gradeGroupModel);
            createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> DeleteAsync(int id)  
        {
            int removedElements;
            var gradeGroup = await _dbContext.GradeGroups.FindAsync(id);

            if (gradeGroup == null)
            {
                throw new Exception("Grado-Grupo no encontrado");
            }

            _dbContext.GradeGroups.Remove(gradeGroup);

            removedElements = await _dbContext.SaveChangesAsync();

            return removedElements;
        }

        public async Task<IEnumerable<GradeGroup>> GetAllAsync()
        {
            return await _dbContext.GradeGroups.Select(g => new GradeGroup
            {
                Id = g.Id,
                GradeId = g.GradeId,
                GroupId = g.GroupId,
                Grade = new Grade { Id = g.Grade.Id, Name = g.Grade.Name },
                Group = new Group { Id = g.Group.Id, Name = g.Group.Name } 
            }).ToListAsync();
        }

        public async Task<GradeGroup?> GetByIdAsync(int id)
        {
            var gradeGroupModel = await _dbContext.GradeGroups.FirstOrDefaultAsync(p => p.Id == id);
            if (gradeGroupModel != null)
            {
                return new GradeGroup
                {
                    Id = gradeGroupModel.Id,
                    GradeId = gradeGroupModel.GradeId,
                    GroupId= gradeGroupModel.GroupId,
                    Grade = new Grade 
                    {
                        Id = gradeGroupModel.Grade.Id, 
                        Name = gradeGroupModel.Grade.Name
                    },
                    Group = new Group 
                    { 
                        Id = gradeGroupModel.Group.Id, 
                        Name = gradeGroupModel.Group.Name 
                    }
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<int> UpdateAsync(GradeGroup gradeGroup)
        {
            int updatedElements;
            var gradeGroupFound = await _dbContext.GradeGroups.FindAsync(gradeGroup.Id);

            if (gradeGroupFound == null)
            {
                throw new Exception("Grado-Grupo no encontrado.");
            }

            gradeGroupFound!.Id = gradeGroup.Id;
            gradeGroupFound.GradeId = gradeGroup.GradeId;
            gradeGroupFound.GroupId = gradeGroup.GroupId;

            updatedElements = await _dbContext.SaveChangesAsync();
            return updatedElements;
        }
    }
}
