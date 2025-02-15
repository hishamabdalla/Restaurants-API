using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Specification
{
    public class RestaurantSpecification:BaseSpecification<Restaurant,int>
    {
        public RestaurantSpecification(int id):base(R=>R.Id==id)
        {
            ApplyIncludes();
        }

        public RestaurantSpecification(int pageSize,int pageIndex,string search):base
        (
            R=>string.IsNullOrEmpty(search)||R.Name.ToLower().Contains(search.ToLower())
        )
        {
            ApplyIncludes();
            ApplyPaging(pageSize*(pageIndex-1),pageSize);

        }

        private void ApplyIncludes()
        {
            AddInclude(r => r.Dishes);
        }

       
    }
}
