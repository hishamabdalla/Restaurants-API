﻿using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Constant;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
    IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
                user.Email,
                resourceOperation,
                restaurant.Name);
            if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/read operation - successful authorization");
                return true;
            }
            if (resourceOperation == ResourceOperation.Delete && user.InRole(UserRole.Admin))
            {
                logger.LogInformation("Admin user, delete operation - successful authorization");
                return true;
            }
            if ((resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update)
                && user.Id == restaurant.OwnerId)
            {
                logger.LogInformation("Restaurant owner - successful authorization");
                return true;
            }
            return false;
        }
    }
}