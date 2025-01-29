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

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteRestaurantCommandHandler(IUnitOfWork unitOfWork,IRestaurantAuthorizationService restaurantAuthorizationService)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant=await _unitOfWork.Repository<Restaurant,int>().GetByIdAsync(request.Id);
            if (restaurant == null)
            {
                return false;
            }

            if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
                throw new ForbidException();

            await _unitOfWork.Repository<Restaurant, int>().Delete(restaurant);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
