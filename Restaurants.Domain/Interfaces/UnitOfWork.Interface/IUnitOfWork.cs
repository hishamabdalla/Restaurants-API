using Restaurants.Domain.Interfaces.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Interfaces.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity :class;
    }
}
