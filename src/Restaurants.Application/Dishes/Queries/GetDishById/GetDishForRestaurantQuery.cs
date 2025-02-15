using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Dishes.DishDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishById
{
    public class GetDishForRestaurantQuery(int RestaurantId,int DishId):IRequest<ApiResponse<DishDto>>
    {
        public int DishId { get; } = DishId;
        public int RestaurantId { get; }=RestaurantId;
    }
}
