using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Restaurants.APITests;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using System.Net;
using Xunit;

namespace Restaurants.API.Controllers.Tests
{
    public class RestaurantsControllerTests:IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<IUnitOfWork> _unitOfWork= new();

        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            //_factory = factory.WithWebHostBuilder(builder =>
            //{
            //    builder.ConfigureServices(services =>
            //    {
            //        services.AddSingleton<IPolicyEvaluator,FakePolicyEvaluator>();
            //        services.Replace(ServiceDescriptor.Scoped(typeof(IUnitOfWork),
            //                                                _ => _unitOfWork.Object));
            //    });
            //});
            _factory = factory;

        }

        
        [Fact]
        public async Task GetAll_ForValidRequest_Return200Ok()
        {
           
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/api/Restaurants");
           
            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task GetById_ForValidRequest_Return200Ok()
        {
            //Arrange

            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("/api/Restaurants/1");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetById_ForNotExistingId_ShouldReturn404NotFound()
        {
            //Arrange
            _unitOfWork.Setup(x => x.Repository<Restaurant, int>().GetByIdAsync(100)).ReturnsAsync((Restaurant?)null);
             
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Restaurants/100");

            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
         }

       

    }
}