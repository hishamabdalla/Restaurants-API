using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.DishDtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishById
{
    public class GetDishForRestaurantQueryHandler : IRequestHandler<GetDishForRestaurantQuery, DishDto>
    {
        private readonly ILogger<GetDishForRestaurantQueryHandler> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetDishForRestaurantQueryHandler(ILogger<GetDishForRestaurantQueryHandler> logger,IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<DishDto> Handle(GetDishForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Retrieving dish: {DishId}, for restaurant with id: {RestaurantId}",
             request.DishId,
             request.RestaurantId);

            var restaurant= await unitOfWork.Repository<Restaurant,int>().GetByIdAsync(request.RestaurantId);
            //if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            var dish=restaurant.Dishes.FirstOrDefault(d=>d.Id==request.DishId);
            //if (dish == null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());
             
            var result=mapper.Map<DishDto>(dish);

            return result;


        }
    }
}
