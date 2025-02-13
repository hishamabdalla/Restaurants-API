using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Dishes.DishDtos;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.RestaurantDtos;
using Restaurants.Domain.Entities;
using Xunit;

namespace Restaurants.Application.Restaurants.DTOs.Tests
{
    public class RestaurantProfileTests
    {
        private IMapper _mapper;

        public RestaurantProfileTests()
        {
            var configruation = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantProfile>();
            });

             _mapper = configruation.CreateMapper();
        }

        [Fact()]
        public void CreateMap_ForRestaurantToRestaurantDto_MapsCoorectly()
        {
            //arrange
           
            
            var restaurant = new Restaurant
            {
                Id = 1,
                Name = "Restaurant",
                Description = "Description",
                Category = "Category",
                ContactEmail = "test@test.com",
                HasDelivery = true,
                ContactNumber = "123456789",
                Address = new Address
                {
                    City = "City",
                    PostalCode = "11-111",
                    Street = "Street"
                }
            };

            //act
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            //assert
            restaurantDto.Should().NotBeNull();
            restaurantDto.Id.Should().Be(restaurant.Id);
            restaurantDto.Name.Should().Be(restaurant.Name);
            restaurantDto.Description.Should().Be(restaurant.Description);
            restaurantDto.Category.Should().Be(restaurant.Category);
            restaurantDto.ContactEmail.Should().Be(restaurant.ContactEmail);
            restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
            restaurantDto.ContactNumber.Should().Be(restaurant.ContactNumber);
            restaurantDto.Street.Should().Be(restaurant.Address.Street);
            restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);
            restaurantDto.City.Should().Be(restaurant.Address.City);
           


        }

        [Fact()]
        public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCoorectly()
        {
            //arrange
            var createRestaurantCommand = new CreateRestaurantCommand
            {
                Name = "Restaurant",
                Description = "Description",
                Category = "Category",
                ContactEmail = "test@test.com",
                HasDelivery = true,
                ContactNumber = "123456789",
                City = "City",
                PostalCode = "11-111",
                Street = "Street"



            };

            //act

            var restaurant = _mapper.Map<Restaurant>(createRestaurantCommand);

            //assert
            restaurant.Should().NotBeNull();
            restaurant.Name.Should().Be(createRestaurantCommand.Name);
            restaurant.Description.Should().Be(createRestaurantCommand.Description);
            restaurant.Category.Should().Be(createRestaurantCommand.Category);
            restaurant.ContactEmail.Should().Be(createRestaurantCommand.ContactEmail);
            restaurant.HasDelivery.Should().Be(createRestaurantCommand.HasDelivery);
            restaurant.ContactNumber.Should().Be(createRestaurantCommand.ContactNumber);
            restaurant.Address.Should().NotBeNull();
            restaurant.Address.City.Should().Be(createRestaurantCommand.City);
            restaurant.Address.PostalCode.Should().Be(createRestaurantCommand.PostalCode);
            restaurant.Address.Street.Should().Be(createRestaurantCommand.Street);




        }

        [Fact()]
        public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCoorectly()
        {
            //arrange
            var updateRestaurantCommand = new UpdateRestaurantCommand(1)
            {
                Name = "Restaurant",
                Description = "Description",
                Category = "Category",
                HasDelivery = true
            };

            var restaurant = _mapper.Map<Restaurant>(updateRestaurantCommand);

            //assert
            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(updateRestaurantCommand.Id);
            restaurant.Name.Should().Be(updateRestaurantCommand.Name);
            restaurant.Description.Should().Be(updateRestaurantCommand.Description);
            restaurant.Category.Should().Be(updateRestaurantCommand.Category);
            restaurant.HasDelivery.Should().Be(updateRestaurantCommand.HasDelivery);

        }
    }
}