using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.Repositories.Interfaces;
using Restaurants.Domain.Specification;
using Restaurants.Infrastructure.Data.Contexts;
using Restaurants.Infrastructure.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    public class GenericRepository<TEntity,TKey> : IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly RestaurantsDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(RestaurantsDbContext context)
        {
            _context = context;
            _dbSet= _context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Restaurant))
            {
                return await _context.Restaurants.Include(d => d.Dishes).ToListAsync() as IEnumerable<TEntity>;
            }
            return await _dbSet.ToListAsync();
        }
        public async Task<TKey> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(TEntity  entity)
        {

            _dbSet.Remove(entity);

        }       
     

        public async Task<TEntity?> GetByIdAsync(int? id)
        {
            if (typeof(TEntity) == typeof(Restaurant))
            {
                return await _context.Restaurants.Include(d => d.Dishes).FirstOrDefaultAsync(i=>i.Id==id) as TEntity;
            }
            return await _dbSet.FindAsync(id);

        }

        public  Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;

        }

        public async Task<IEnumerable<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity,TKey> specification)
        {
          return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<TEntity> GetByIdWithSpecification(ISpecification<TEntity, TKey> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }
        public async Task<int> GetCountAsync(ISpecification<TEntity, TKey> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }
        private  IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TKey> specification)
        {
            return  SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specification);
        }

       
    }
}
