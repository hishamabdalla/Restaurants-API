﻿using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
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
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Restaurant))
            {
                return await _context.Restaurants.Include(d => d.Dishes).ToListAsync() as IEnumerable<TEntity>;
            }
            return await _dbSet.ToListAsync();
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
    }
}
