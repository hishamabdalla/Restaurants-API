using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Requierments
{
    public class CreatedMultipleRestaurantsRequirementHandler : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserContext userContext;

        public CreatedMultipleRestaurantsRequirementHandler(IUnitOfWork unitOfWork,IUserContext userContext)
        {
            this.unitOfWork = unitOfWork;
            this.userContext = userContext;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
        {
            var currentUser = userContext.CurrentUser();

            var restaurants = await unitOfWork.Repository<Restaurant, int>().GetAllAsync();

            var userRestaurantCreated=restaurants.Count(r=>r.OwnerId==currentUser!.Id);

            if(userRestaurantCreated >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
