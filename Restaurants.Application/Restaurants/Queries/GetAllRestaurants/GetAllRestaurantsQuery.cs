using MediatR;
using Restaurants.Application.Restaurants.RestaurantDtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<IEnumerable< RestaurantDto>>
{

}
