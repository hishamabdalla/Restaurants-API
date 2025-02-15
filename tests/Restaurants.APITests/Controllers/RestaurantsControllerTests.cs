using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using Restaurants.APITests;
using Restaurants.Application.Restaurants.RestaurantDtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using Restaurants.Domain.Specification;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Restaurants.API.Controllers.Tests
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IUnitOfWork> _unitOfWork = new();
        private readonly Mock<ISpecification<Restaurant, int>> spec = new();

        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Ensure authorization is bypassed for testing
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    // Replace IUnitOfWork with a mock instance
                    services.Replace(ServiceDescriptor.Scoped(typeof(IUnitOfWork), _ => _unitOfWork.Object));
                });
            });
        }


        [Fact]
        public async Task GetById_ForExistingId_ShouldReturn200Ok()
        {
            // Arrange
            var id = 1;

            var restaurant = new Restaurant { Id = id, Name = "Restaurant A", Description = "Test A" };

            _unitOfWork
                .Setup(m => m.Repository<Restaurant, int>().GetByIdWithSpecification(It.IsAny<RestaurantSpecification>()))
                .ReturnsAsync(restaurant);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/restaurants/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_ForNonExistingId_ShouldReturn404NotFound()
        {
            // Arrange
            var id = 1123;

            var restaurant = new Restaurant { Id = id, Name = "Restaurant A", Description = "Test A" };

            _unitOfWork
                .Setup(m => m.Repository<Restaurant, int>().GetByIdAsync(id))
                .ReturnsAsync(restaurant);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/restaurants/{id}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }



        [Fact]
        public async Task GetAll_ForValidRequest_Returns200Ok()
        {
            // Arrange
            var restaurants = new List<Restaurant>
        {
        new Restaurant { Id = 1, Name = "Restaurant A", Description = "Test A" },
        new Restaurant { Id = 2, Name = "Restaurant B", Description = "Test B" }
      };

            // Mock GetAllWithSpecificationAsync instead of GetAllAsync
            _unitOfWork
                .Setup(m => m.Repository<Restaurant, int>().GetAllWithSpecificationAsync(It.IsAny<RestaurantSpecification>()))
                .ReturnsAsync(restaurants);

            var client = _factory.CreateClient();

            // Act
            var result = await client.GetAsync("/api/restaurants?pageNumber=1&pageSize=10");

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        

    }
}
