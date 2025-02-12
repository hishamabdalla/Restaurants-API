using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Exceptions;
using Restaurants.Application.User;
using Restaurants.Domain.Constant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using Restaurants.Infrastructure.Authorization.Services;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand,int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserContext userContext;
    private readonly IRestaurantAuthorizationService restaurantAuthorizationService;
    private readonly ILogger _logger;

    public CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,IUnitOfWork unitOfWork, IMapper mapper,IUserContext userContext,IRestaurantAuthorizationService restaurantAuthorizationService )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        this.userContext = userContext;
        this.restaurantAuthorizationService = restaurantAuthorizationService;
        _logger = logger;
    }
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        _logger.LogInformation("{UserEmail} [{UserId}] is creating a new restaurant {@Restaurant}",currentUser.Email,currentUser.Id, request); 
        
        var restaurant = _mapper.Map<Restaurant>(request);
        restaurant.OwnerId=currentUser.Id;

        if (!restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Create))
            throw new ForbidException();

        await _unitOfWork.Repository<Restaurant, int>().AddAsync(restaurant);
       await _unitOfWork.CompleteAsync();
        return  restaurant.Id;
    } 
}
