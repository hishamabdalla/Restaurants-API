using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Dishes.DishDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetAll
{
    public class GetAllDishesForRestaurantQuery(int RestaurantId) : IRequest<ApiResponse<IEnumerable<DishDto>>>
    {
        public int RestaurantId { get; } = RestaurantId;
    }
}
