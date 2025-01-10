using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Interfaces.Repositories.Interfaces;
using Restaurants.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly RestaurantsDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(RestaurantsDbContext context)
        {
            _context = context;
            _dbSet= _context.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(int? id)
        {
            
            var entity =  await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);

        }       
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
           return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int? id)
        {
            return await _dbSet.FindAsync(id);

        }

        public  Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;

        }
    }
}
