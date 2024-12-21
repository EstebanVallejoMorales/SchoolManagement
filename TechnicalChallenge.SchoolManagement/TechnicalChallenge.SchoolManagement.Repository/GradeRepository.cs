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
    public class GradeRepository: IRepository<Grade>
    {
        private readonly AppDbContext _dbContext;

        public GradeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> AddAsync(Grade entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Grade>> GetAllAsync()
        {
            return await _dbContext.Grades.Select(s => new Grade
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
        }

        public Task<Grade?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Grade entity)
        {
            throw new NotImplementedException();
        }
    }
}
