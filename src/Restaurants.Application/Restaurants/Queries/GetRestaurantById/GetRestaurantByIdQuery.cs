using MediatR;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.RestaurantDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQuery:IRequest<ApiResponse<RestaurantDto>>
    {
        public GetRestaurantByIdQuery(int id)
        {
            
            this.Id = id;
        }
        public int Id { get;}
    }
}
