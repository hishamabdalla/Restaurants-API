using AutoMapper;
using MediatR;
using Restaurants.Application.Common.Pagination;
using Restaurants.Application.Restaurants.RestaurantDtos;
using Restaurants.Application.URl.Services;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using Restaurants.Domain.Specification;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantQueryHandler : IRequestHandler<GetAllRestaurantsQuery, PagedResponse<IEnumerable< RestaurantDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUriService uriService;
    private readonly IMapper _mapper;
    public GetAllRestaurantQueryHandler(IMapper mapper, IUnitOfWork unitOfWork,IUriService uriService)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        this.uriService = uriService;
    }

    public async Task<PagedResponse<IEnumerable<RestaurantDto>>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var route = $"/api/{nameof(Restaurants)}";
        var spec = new RestaurantSpecification(request.PageSize,request.PageNumber,request.Search);
        var dto=  _mapper.Map<IEnumerable<RestaurantDto>>(await _unitOfWork.Repository<Restaurant, int>().GetAllWithSpecificationAsync(spec));

        var countSpec = new RestaurantWithCountSpecification(request.Search);
        var count= await _unitOfWork.Repository<Restaurant, int>().GetCountAsync(countSpec);

        return PaginationHelper.CreatePagedReponse<RestaurantDto>(dto,request.PageNumber,request.PageSize,count,uriService, route);

    }
}
