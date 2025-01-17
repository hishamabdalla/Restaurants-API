using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
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
        public DeleteRestaurantCommandHandler(IUnitOfWork unitOfWork)
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
             await _unitOfWork.Repository<Restaurant, int>().Delete(restaurant);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
