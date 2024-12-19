using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalChallenge.SchoolManagement.UseCases.Interfaces
{
    public interface IRepository<TEntity>
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);

    }
}
