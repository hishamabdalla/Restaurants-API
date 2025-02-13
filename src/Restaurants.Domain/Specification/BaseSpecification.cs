using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Specification
{
    public class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
       public  Expression<Func<TEntity, bool>> Criteria { get; }=null; 
       public List<Expression<Func<TEntity, object>>> Includes { get; }=new List<Expression<Func<TEntity, object>>>();
        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagingEnabled { get; set; }

        public BaseSpecification() { }
        
        public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }
        
        public void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
          Includes.Add(includeExpression);
        }

        public void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

    }
}
