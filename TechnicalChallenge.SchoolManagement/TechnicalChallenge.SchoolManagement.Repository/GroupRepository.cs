using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TechnicalChallenge.SchoolManagement.Data;
using TechnicalChallenge.SchoolManagement.Entities;
using TechnicalChallenge.SchoolManagement.Models;
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

        public async Task<int> AddAsync(Entities.Group group)
        {
            int createdElements;
            var groupModel = new GroupModel
            {
                Name = group.Name
            };

            await _dbContext.Groups.AddAsync(groupModel);
            createdElements = await _dbContext.SaveChangesAsync();
            return createdElements;
        }

        public async Task<int> UpdateAsync(Entities.Group group)
        {
            int updatedElements;
            var groupFound = await _dbContext.Groups.FindAsync(group.Id);

            if (group == null)
            {
                throw new Exception("Grupo no encontrado");
            }

            groupFound!.Id = group.Id;
            groupFound.Name = group.Name;

            updatedElements = await _dbContext.SaveChangesAsync();
            return updatedElements;
        }

        public async Task<IEnumerable<Entities.Group>> GetAllAsync()
        {
            return await _dbContext.Groups.Select(s => new Entities.Group
            {
               Id = s.Id,
               Name = s.Name
            }).ToListAsync();
        }

        public async Task<Entities.Group?> GetByIdAsync(int id)
        {
            var groupModel = await _dbContext.Groups.FirstOrDefaultAsync(p => p.Id == id);
            if (groupModel != null)
            {
                return new Entities.Group
                {
                    Name = groupModel.Name,
                    Id = groupModel.Id
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<int> DeleteAsync(int groupId)
        {
            int removedElements;
            var group = await _dbContext.Groups.FindAsync(groupId);

            if (group == null)
            {
                throw new Exception("Grupo no encontrado");
            }

            _dbContext.Groups.Remove(group);

            removedElements = await _dbContext.SaveChangesAsync();

            return removedElements;
        }
    }
}
