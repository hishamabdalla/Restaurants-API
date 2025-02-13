using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Specification
{
    public class SpecificationEvaluator<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity, TKey> specification)
        {
            var query = inputQuery;

            if(specification.Criteria != null)
            {
                query=query.Where(specification.Criteria);
            }
            if (specification.IsPagingEnabled)
            {
                query=query.Skip(specification.Skip);
                query=query.Take(specification.Take);
            }

            query = specification.Includes.Aggregate(query, (current, include) =>current.Include(include));

            return query;
        }

         
    }
}
