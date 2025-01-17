using AutoMapper;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand,int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateRestaurantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    async Task<int> IRequestHandler<CreateRestaurantCommand, int>.Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurent = _mapper.Map<Restaurant>(request);
        var id = await _unitOfWork.Repository<Restaurant, int>().AddAsync(restaurent);
        return id;
    }
}
