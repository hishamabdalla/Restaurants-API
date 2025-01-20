using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand,int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger= logger;
    }
    async Task<int> IRequestHandler<CreateRestaurantCommand, int>.Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating a new restaurant {@Restaurant}",request);
        var restaurent = _mapper.Map<Restaurant>(request);
        var id = await _unitOfWork.Repository<Restaurant, int>().AddAsync(restaurent);
        return id;
    }
}
