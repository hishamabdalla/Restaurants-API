using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand>
    {
        private readonly ILogger<CreateDishCommandHandler> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new Dish:{@DishRequest}", request);

            var restaurant= await unitOfWork.Repository<Dish,int>().GetByIdAsync(request.RestaurantId);
            if (restaurant == null)
            {
                throw new ArgumentException();
            }
             var dishMap=mapper.Map<Dish>(request);

            await unitOfWork.Repository<Dish,int>().AddAsync(dishMap);

        }
    }
}
