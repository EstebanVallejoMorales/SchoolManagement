using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Data;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Repository
{
    public class GenderRepository : IRepository<Gender>
    {
        private readonly AppDbContext _dbContext;

        public GenderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> AddAsync(Gender entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Gender>> GetAllAsync()
        {
            return await _dbContext.Genders.Select(s => new Gender
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
        }

        public Task<Gender?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Gender entity)
        {
            throw new NotImplementedException();
        }
    }
}
