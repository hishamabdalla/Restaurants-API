using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.User;
using Restaurants.Domain.Constant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.Repositories.Interfaces;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using Restaurants.Infrastructure.Authorization.Services;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handel_ForValidCommand_RestaurantCreatedRestaurantId()
        {
            //Arrange
            var logger = new Mock<ILogger<CreateRestaurantCommandHandler>>();
            var mapper = new Mock<IMapper>();

            var unitOfWork = new Mock<IUnitOfWork>();

            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurant();
                restaurant.Id = 1;


            mapper.Setup(x => x.Map<Restaurant>(command)).Returns(restaurant);


            unitOfWork.Setup(x => x.Repository<Restaurant, int>().AddAsync(It.IsAny<Restaurant>())).Returns(Task.CompletedTask);
            unitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);


            
            var userContext = new Mock<IUserContext>();
            var currentUser = new CurrentUser("1", "Test@test.com", [],null,null);
            userContext.Setup(x => x.GetCurrentUser()).Returns(currentUser);
            
            var restaurantAuthorizationService = new Mock<IRestaurantAuthorizationService>();
            restaurantAuthorizationService.Setup(x => x.Authorize(restaurant, ResourceOperation.Create)).Returns(true);

            var commandHandler = new CreateRestaurantCommandHandler(logger.Object, unitOfWork.Object, mapper.Object, userContext.Object, restaurantAuthorizationService.Object);

            //Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //Assert
            result.Should().Be(restaurant.Id);  
            restaurant.OwnerId
                .Should().Be(currentUser.Id);
            unitOfWork.Verify(x => x.Repository<Restaurant, int>().AddAsync(restaurant), Times.Once);


        }
    }
}