using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Specification
{
    public class RestaurantWithCountSpecification : BaseSpecification<Restaurant, int>
    {
        public RestaurantWithCountSpecification(string? search):base
        (
            R=>string.IsNullOrEmpty(search)||R.Name.ToLower().Contains(search.ToLower())
        )
        {
        }
    }
}