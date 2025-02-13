using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.Repositories.Interfaces;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using Restaurants.Infrastructure.Data.Contexts;
using Restaurants.Infrastructure.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly RestaurantsDbContext _context;
        private Hashtable _repositories;
        public UnitOfWork(RestaurantsDbContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }

        public async Task CompleteAsync() => await _context.SaveChangesAsync();

        public IGenericRepository<TEntity,TKey> Repository<TEntity,TKey>() where TEntity : BaseEntity<TKey>
        {
            if (!_repositories.ContainsKey(typeof(TEntity).Name))
            {
                var repository = new GenericRepository<TEntity,TKey>(_context);
                _repositories.Add(typeof(TEntity).Name, repository);
            }

            return _repositories[typeof(TEntity).Name] as IGenericRepository<TEntity,TKey>;
        }


    }
}

