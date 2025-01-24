using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDish
{
    public class DeleteDishCommandHandler : IRequestHandler<DeleteDishCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteDishCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteDishCommand request, CancellationToken cancellationToken)
        {
            var restaurant= await unitOfWork.Repository<Restaurant,int>().GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                return false;
            }

            var dish=restaurant.Dishes.FirstOrDefault(i=>i.Id==request.DishId);
            if(dish == null)
            {
                return false;
            }

            await unitOfWork.Repository<Dish,int>().Delete(dish);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
