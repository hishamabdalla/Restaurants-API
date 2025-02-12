using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
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

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand,int>
    {
        private readonly ILogger<CreateDishCommandHandler> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IRestaurantAuthorizationService restaurantAuthorizationService;

        public CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,IUnitOfWork unitOfWork,IMapper mapper,IRestaurantAuthorizationService? restaurantAuthorizationService)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.restaurantAuthorizationService = restaurantAuthorizationService;
        }
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new Dish:{@DishRequest}", request);

            var restaurant= await unitOfWork.Repository<Restaurant,int>().GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new ArgumentException();
            }

            //if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Create))
            //    throw new ForbidException();
            var dishMap=mapper.Map<Dish>(request);

           await unitOfWork.Repository<Dish,int>().AddAsync(dishMap);
            
            await unitOfWork.CompleteAsync();
            return dishMap.Id;
          
        }
    }
}
