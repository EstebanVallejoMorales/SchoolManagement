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
    public class GradeRepository : IRepository<Grade>
    {
        private readonly AppDbContext _dbContext;

        public GradeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(Grade grade)
        {
            int createdElements;
            var gradeModel = new GradeModel
            {
                Name = grade.Name
            };

            await _dbContext.Grades.AddAsync(gradeModel);
            createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> UpdateAsync(Grade grade)
        {
            int updatedElements;
            var gradeFound = await _dbContext.Grades.FindAsync(grade.Id);

            if (grade == null)
            {
                throw new Exception("Grado no encontrado");
            }

            gradeFound!.Id = grade.Id;
            gradeFound.Name = grade.Name;
            
            updatedElements = await _dbContext.SaveChangesAsync();
            return updatedElements;
        }

        public async Task<IEnumerable<Grade>> GetAllAsync()
        {
            return await _dbContext.Grades.Select(s => new Grade
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();
        }

        public async Task<Grade?> GetByIdAsync(int id)
        {
            var gradeModel = await _dbContext.Grades.FirstOrDefaultAsync(p => p.Id == id);
            if (gradeModel != null)
            {
                return new Grade
                {                    
                    Name = gradeModel.Name,                    
                    Id = gradeModel.Id
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<int> DeleteAsync(int gradeId)
        {
            int removedElements;
            var grade = await _dbContext.Grades.FindAsync(gradeId);

            if (grade == null)
            {
                throw new Exception("Grado no encontrado");
            }

            _dbContext.Grades.Remove(grade);

            removedElements = await _dbContext.SaveChangesAsync();

            return removedElements;
        }
    }
}
