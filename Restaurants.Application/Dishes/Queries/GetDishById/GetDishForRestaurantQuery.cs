using MediatR;
using Restaurants.Application.Dishes.DishDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishById
{
    public class GetDishForRestaurantQuery(int RestaurantId,int DishId):IRequest<DishDto>
    {
        public int DishId { get; } = DishId;
        public int RestaurantId { get; }=RestaurantId;
    }
}
