using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Data;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Repository
{
    public class GroupRepository : IRepository<Entities.Group>
    {
        private readonly AppDbContext _dbContext;

        public GroupRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> AddAsync(Entities.Group entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Entities.Group>> GetAllAsync()
        {
            return await _dbContext.Groups.Select(s => new Entities.Group
            {
               Id = s.Id,
               Name = s.Name
            }).ToListAsync();
        }

        public Task<Entities.Group?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Entities.Group entity)
        {
            throw new NotImplementedException();
        }
    }
}
