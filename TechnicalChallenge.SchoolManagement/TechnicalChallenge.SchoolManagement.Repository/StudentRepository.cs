using Microsoft.EntityFrameworkCore;
using TechnicalChallenge.SchoolManagement.Data;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.UseCases.Interfaces;

namespace TechnicalChallenge.SchoolManagement.Repository
{
    public class StudentRepository : IRepository<Student>
    {
        private readonly AppDbContext _dbContext;

        public StudentRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Student student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _dbContext.Students.FindAsync(id);
        }
    }
}
