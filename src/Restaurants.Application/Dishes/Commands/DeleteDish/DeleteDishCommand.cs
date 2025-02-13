using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommand(int restaurantId,int dishId):IRequest<bool>
    {
        public int RestaurantId { get;  }=restaurantId;
        public int DishId { get; } =dishId;

    }
}
