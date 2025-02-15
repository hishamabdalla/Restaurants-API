using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Dishes.DishDtos;
using Restaurants.Application.Exceptions;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetAll
{
    public class GetAllDishesForRestaurantQueryHandler : IRequestHandler<GetAllDishesForRestaurantQuery, ApiResponse<IEnumerable<DishDto>>>
    {
        private readonly ILogger<GetAllDishesForRestaurantQueryHandler> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllDishesForRestaurantQueryHandler(ILogger<GetAllDishesForRestaurantQueryHandler> logger,IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<IEnumerable<DishDto>>> Handle(GetAllDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting All Dishes");

            var restaurants = await unitOfWork.Repository<Restaurant, int>().GetByIdAsync(request.RestaurantId);
            if(restaurants == null)
            {
                throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
            }

            if (restaurants.Dishes.Any())
            {
                var dishes = mapper.Map<IEnumerable<DishDto>>(restaurants.Dishes);
                return new ApiResponse<IEnumerable<DishDto>>(dishes);
            }
            else
            {
                return new ApiResponse<IEnumerable<DishDto>>(success: false, statusCode: 404, message: "No Dishes Found");
            }




        }

    }
}
