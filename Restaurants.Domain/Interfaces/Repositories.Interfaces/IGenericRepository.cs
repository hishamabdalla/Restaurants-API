using Restaurants.Domain.Entities;
using Restaurants.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Interfaces.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int? id);

        Task<IEnumerable<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity, TKey> specification);
        Task<TEntity> GetByIdWithSpecification(ISpecification<TEntity, TKey> specification);
        Task<int> GetCountAsync(ISpecification<TEntity,TKey> specification);
        Task AddAsync(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);

    }
}
