using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Exceptions;
using Restaurants.Application.Restaurants.RestaurantDtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        public GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;

        }
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting restaurant {RestaurantId}",request.Id);
            var restaurant = await _unitOfWork.Repository<Restaurant, int>().GetByIdAsync(request.Id);
            //if (restaurant == null)
            //{
            //    throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
            //}
            return _mapper.Map<RestaurantDto>(restaurant);
        }
    }
}
