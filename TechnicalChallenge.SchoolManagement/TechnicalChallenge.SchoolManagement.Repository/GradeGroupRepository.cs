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

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
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

        public Task<GradeGroup?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(GradeGroup entity)
        {
            throw new NotImplementedException();
        }
    }
}
