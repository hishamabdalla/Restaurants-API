using AutoMapper;
using MediatR;
using Restaurants.Application.Exceptions;
using Restaurants.Domain.Constant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using Restaurants.Infrastructure.Authorization.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.UpdateDish
{
    public class UpdateDishCommandHandler : IRequestHandler<UpdateDishCommand, bool>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IRestaurantAuthorizationService restaurantAuthorizationService;

        public UpdateDishCommandHandler(IUnitOfWork unitOfWork,IMapper mapper,IRestaurantAuthorizationService restaurantAuthorizationService )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.restaurantAuthorizationService = restaurantAuthorizationService;
        }
        async Task<bool> IRequestHandler<UpdateDishCommand, bool>.Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var restaurant= await unitOfWork.Repository<Restaurant,int>().GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new ArgumentException();
            }
            var dish=restaurant.Dishes.FirstOrDefault(i=>i.Id==request.DishId);
            if(dish == null)
            {
                return false;
            }

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbidException();
            mapper.Map(request, dish);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
