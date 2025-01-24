using AutoMapper;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
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

        public UpdateDishCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        async Task<bool> IRequestHandler<UpdateDishCommand, bool>.Handle(UpdateDishCommand request, CancellationToken cancellationToken)
        {
            var restaurnt= await unitOfWork.Repository<Restaurant,int>().GetByIdAsync(request.RestaurantId);
            if (restaurnt == null)
            {
                throw new ArgumentException();
            }
            var dish=restaurnt.Dishes.FirstOrDefault(i=>i.Id==request.DishId);
            if(dish == null)
            {
                return false;
            }
            mapper.Map(request, dish);
            await unitOfWork.CompleteAsync();
            return true;
        }
    }
}
