using Xunit;
using Restaurants.Infrastructure.Authorization.Requierments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using FluentAssertions;

namespace Restaurants.Infrastructure.Authorization.Requierments.Tests
{
    public class CreatedMultipleRestaurantsRequirementHandlerTests
    {
        [Fact()]
        public async Task HandelRequirementAsync_UseHasCreatedMultioleRestaurant_ShouldSucceed()
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);

            var userContext = new Mock<IUserContext>();
            userContext.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var restaurants = new List<Restaurant>
            {
                new Restaurant()
                {
                    OwnerId=currentUser.Id
                },
                new Restaurant {OwnerId =currentUser.Id},
                new Restaurant {OwnerId = "2"},
            };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.Repository<Restaurant, int>().GetAllAsync()).ReturnsAsync(restaurants); // This is the setup for the GetAllAsync method

            var requirement = new CreatedMultipleRestaurantsRequirement(2);

            var handler = new CreatedMultipleRestaurantsRequirementHandler(unitOfWork.Object, userContext.Object);

            // act
            var context = new AuthorizationHandlerContext([requirement], null, null);

            await handler.HandleAsync(context);

            // assert

            context.HasSucceeded.Should().BeTrue();

        }

        [Fact()]
        public async Task HandelRequirementAsync_UseHasNotCreatedMultioleRestaurant_ShouldFail()
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);

            var userContext = new Mock<IUserContext>();
            userContext.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var restaurants = new List<Restaurant>
            {
                new Restaurant()
                {
                    OwnerId=currentUser.Id
                },
                new Restaurant {OwnerId =currentUser.Id},
                new Restaurant {OwnerId = "2"},
            };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(u => u.Repository<Restaurant, int>().GetAllAsync()).ReturnsAsync(restaurants); // This is the setup for the GetAllAsync method

            var requirement = new CreatedMultipleRestaurantsRequirement(3);

            var handler = new CreatedMultipleRestaurantsRequirementHandler(unitOfWork.Object, userContext.Object);

            // act
            var context = new AuthorizationHandlerContext([requirement], null, null);

            await handler.HandleAsync(context);

            // assert

            context.HasSucceeded.Should().BeFalse();
        }

    }
}