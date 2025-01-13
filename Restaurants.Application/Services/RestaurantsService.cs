using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.Services.Interfaces;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Services
{
    public class RestaurantsService : IRestaurantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RestaurantsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Restaurant>> GetAllRestaurants()
        {
           return await _unitOfWork.Repository<Restaurant>().GetAllAsync();
        }

        public async Task<Restaurant> GetById(int id)
        {
            return await _unitOfWork.Repository<Restaurant>().GetByIdAsync(id);
        }
    }
}
