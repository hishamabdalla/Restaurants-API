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

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRestaurantAuthorizationService restaurantAuthorizationService;

        public UpdateRestaurantCommandHandler(IUnitOfWork unitOfWork,IMapper mapper,IRestaurantAuthorizationService restaurantAuthorizationService )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.restaurantAuthorizationService = restaurantAuthorizationService;
        }

        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = await _unitOfWork.Repository<Restaurant, int>().GetByIdAsync(request.Id);
            if (restaurant == null)
            {
                return false;
            }

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Update))
                throw new ForbidException();

            _mapper.Map(request,restaurant);
          await  _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
