using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Specification
{
    public interface ISpecification<TEntity,TKey> where TEntity:BaseEntity<TKey>
    {
        Expression<Func<TEntity, bool>> Criteria { get; }
        List<Expression<Func<TEntity, object>>> Includes { get; }

        

        int Take { get; set; }
        int Skip { get; set; }
        bool IsPagingEnabled { get; set; }
    }
}
