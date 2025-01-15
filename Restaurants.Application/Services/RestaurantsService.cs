using AutoMapper;
using Restaurants.Application.DTOs.RestaurantDtos;
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
        private readonly IMapper _mapper;

        public RestaurantsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
           return _mapper.Map<IEnumerable<RestaurantDto>>( await _unitOfWork.Repository<Restaurant>().GetAllAsync());
        }

        public async Task<RestaurantDto> GetById(int id)
        {
            return _mapper.Map<RestaurantDto>( await _unitOfWork.Repository<Restaurant>().GetByIdAsync(id));
        }

       
    }
}
