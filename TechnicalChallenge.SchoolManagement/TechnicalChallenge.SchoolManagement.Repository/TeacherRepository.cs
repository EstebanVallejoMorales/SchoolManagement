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
    public class TeacherRepository: IRepository<Teacher>
    {
        private readonly AppDbContext _dbContext;

        public TeacherRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(Teacher Teacher)
        {
            int createdElements;
            var TeacherModel = new TeacherModel
            {
                GenderId = Teacher.GenderId,
                Name = Teacher.Name,
                LastName = Teacher.LastName
            };

            await _dbContext.Teachers.AddAsync(TeacherModel);
            createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> UpdateAsync(Teacher Teacher)
        {
            int updatedElements;
            var TeacherFound = await _dbContext.Teachers.FindAsync(Teacher.Id);

            if (TeacherFound == null)
            {
                throw new Exception("Teacher not found");
            }

            TeacherFound!.Id = Teacher.Id;
            TeacherFound.Name = Teacher.Name;
            TeacherFound.LastName = Teacher.LastName;
            TeacherFound.GenderId = Teacher.GenderId;
            updatedElements = await _dbContext.SaveChangesAsync();
            return updatedElements;
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync()
        {
            return await _dbContext.Teachers.Select(s => new Teacher
            {
                Id = s.Id,
                Name = s.Name,
                LastName = s.LastName,
                GenderId = s.GenderId,
                Gender = new Gender
                {
                    Id = s.GenderId,
                    Name = s.Gender.Name
                }
            }).ToListAsync();
        }

        public async Task<Teacher?> GetByIdAsync(int id)
        {
            var TeacherModel = await _dbContext.Teachers.Include(s => s.Gender).FirstOrDefaultAsync(p => p.Id == id);
            if (TeacherModel != null)
            {
                return new Teacher
                {
                    GenderId = TeacherModel.GenderId,
                    Name = TeacherModel.Name,
                    LastName = TeacherModel.LastName,
                    Id = TeacherModel.Id,
                    Gender = new Gender
                    {
                        Id = TeacherModel.GenderId,
                        Name = TeacherModel.Gender.Name
                    }
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<int> DeleteAsync(int TeacherId)
        {
            int removedElements;
            var Teacher = await _dbContext.Teachers.FindAsync(TeacherId);

            if (Teacher == null)
            {
                throw new Exception("Teacher not found");
            }

            _dbContext.Teachers.Remove(Teacher);

            removedElements = await _dbContext.SaveChangesAsync();

            return removedElements;
        }
    }
}
