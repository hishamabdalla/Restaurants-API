using Xunit;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using Moq;
using AutoMapper;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Domain.Entities;
using FluentAssertions;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IMapper> _mapper;
        private Mock<IRestaurantAuthorizationService> _restaurantAuthorizationService;

        public UpdateRestaurantCommandHandlerTests()
        {
            _unitOfWork=new Mock<IUnitOfWork>();
            _mapper=new Mock<IMapper>();
            _restaurantAuthorizationService=new Mock<IRestaurantAuthorizationService>();


        }
        [Fact()]
        public void UpdateRestaurantCommandHandlerTest()
        {
            //Arrange


            var restaurant = new Restaurant()
            {
                Id=1,
                Name = "Test",
                Description = "Test",
                OwnerId = "1"
            };

            _unitOfWork.Setup(x => x.Repository<Restaurant, int>().GetByIdAsync(1)).ReturnsAsync(restaurant);

            var command = new UpdateRestaurantCommand(1)
            {
                Name = "Test",
                Description = "Test"
            };    
            



            _restaurantAuthorizationService.Setup(x => x.Authorize(restaurant, Domain.Constant.ResourceOperation.Update)).Returns(true);

            _mapper.Setup(x => x.Map(command, restaurant)).Returns(new Restaurant());
            _unitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

            var commandhandler = new UpdateRestaurantCommandHandler(_unitOfWork.Object,_mapper.Object,_restaurantAuthorizationService.Object);
            //Act
            var result = commandhandler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            result.Result.Should().BeTrue();

        }
    }
}