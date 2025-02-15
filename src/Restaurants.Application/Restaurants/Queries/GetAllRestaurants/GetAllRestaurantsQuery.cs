using MediatR;
using Restaurants.Application.Common.Pagination;
using Restaurants.Application.Restaurants.RestaurantDtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : PaginationQuery<IEnumerable<RestaurantDto>>
{
   public string? Search { get; set; }
}
