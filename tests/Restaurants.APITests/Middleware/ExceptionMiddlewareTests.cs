using Xunit;
using Restaurants.API.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Restaurants.Application.Exceptions;
using FluentAssertions;

namespace Restaurants.API.Middleware.Tests
{
    public class ExceptionMiddlewareTests
    {
        [Fact()]
        public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
        {
            //arrange
            var logger = new Mock<ILogger<ExceptionMiddleware>>();
            var environment = new Mock<IHostEnvironment>();
            var middleware = new ExceptionMiddleware(logger.Object, environment.Object);
            var nextDelegate = new Mock<RequestDelegate>();

            var context = new DefaultHttpContext();

            //act
            await middleware.InvokeAsync(context, nextDelegate.Object);

            //assert
            nextDelegate.Verify(x => x.Invoke(context), Times.Once);
        }

        [Fact()]
        public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCode404()
        {
            var context = new DefaultHttpContext();
            var logger = new Mock<ILogger<ExceptionMiddleware>>();
            var environment = new Mock<IHostEnvironment>();
            var notFoundException = new NotFoundException("Restaurant", "1");

           //act
           var middleware = new ExceptionMiddleware(logger.Object, environment.Object);
            await middleware.InvokeAsync(context, async (httpContext) => throw notFoundException);

            //assert
            context.Response.StatusCode.Should().Be(404);

        }

        [Fact()]
        public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCode403()
        {
            
            var context = new DefaultHttpContext();
            var logger = new Mock<ILogger<ExceptionMiddleware>>();
            var environment = new Mock<IHostEnvironment>();
            var forbidException = new ForbidException();

            //act
            var middleware = new ExceptionMiddleware(logger.Object, environment.Object);
            await middleware.InvokeAsync(context, async (httpContext) => throw forbidException);

            //assert
            context.Response.StatusCode.Should().Be(403);
            

        }
        [Fact()]
        public async Task InvokeAsync_WhenAnyOtherExceptionThrown_ShouldSetStatusCode500()
        {
            var context = new DefaultHttpContext();
            var logger = new Mock<ILogger<ExceptionMiddleware>>();
            var environment = new Mock<IHostEnvironment>();
            var exception = new Exception("An error occured");

            //act
            var middleware = new ExceptionMiddleware(logger.Object, environment.Object);
            await middleware.InvokeAsync(context, async (httpContext) => throw exception);

            //assert
            context.Response.StatusCode.Should().Be(500);
        }

    }
}