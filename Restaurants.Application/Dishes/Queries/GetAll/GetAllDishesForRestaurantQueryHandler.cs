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

namespace Restaurants.Application.Dishes.Queries.GetAll
{
    public class GetAllDishesForRestaurantQueryHandler : IRequestHandler<GetAllDishesForRestaurantQuery, IEnumerable<DishDto>>
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
        public async Task<IEnumerable<DishDto>> Handle(GetAllDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting All Dishes");

            var restaurants = await unitOfWork.Repository<Restaurant, int>().GetByIdAsync(request.RestaurantId);
            if(restaurants == null)
            {
                throw new ArgumentException();
            }

            if(restaurants.Dishes.Count()>0)
            {
                var dishes = mapper.Map<IEnumerable<DishDto>>(restaurants.Dishes);
                return dishes;
            }

            return Enumerable.Empty<DishDto>(); ;
            
        }

    }
}
