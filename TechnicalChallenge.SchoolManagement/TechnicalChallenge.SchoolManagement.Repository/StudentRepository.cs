using Microsoft.EntityFrameworkCore;
using TechnicalChallenge.SchoolManagement.Data;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Models;
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

        public async Task<int> AddAsync(Student student)
        {
            int createdElements;
            var studentModel = new StudentModel
            {
                GenderId = student.GenderId,
                Name = student.Name,
                LastName = student.LastName
            };

            await _dbContext.Students.AddAsync(studentModel);
            createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> UpdateAsync(Student student)
        {
            int updatedElements;
            var studentFound = await _dbContext.Students.FindAsync(student.Id);

            if (student == null)
            {
                throw new Exception("Student not found");
            }

            studentFound!.Id = student.Id;
            studentFound.Name = student.Name;
            studentFound.LastName = student.LastName;
            studentFound.GenderId = student.GenderId;
            updatedElements = await _dbContext.SaveChangesAsync();
            return updatedElements;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _dbContext.Students.Select(s => new Student 
            {
                Id = s.Id,
                Name = s.Name,
                LastName = s.LastName,
                GenderId= s.GenderId
            }).ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            var studentModel = await _dbContext.Students.FindAsync(id);
            return new Student
            {
                GenderId = studentModel.GenderId,
                Name = studentModel.Name,
                LastName = studentModel.LastName,
                Id = studentModel.Id
            };
        }

        public async Task<int> DeleteAsync(int studentId)
        {
            int removedElements;
            var student = await _dbContext.Students.FindAsync(studentId);

            if (student == null)
            {
                throw new Exception("Student not found");
            }
            
            _dbContext.Students.Remove(student);

            removedElements = await _dbContext.SaveChangesAsync();

            return removedElements;
        }
    }
}
