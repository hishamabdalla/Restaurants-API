
using Restaurants.API.Error;
using Restaurants.Application.Exceptions;
using System.Text.Json;

namespace Restaurants.API.Middleware
{
    public class ExceptionMiddleware:IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment environment;

        public ExceptionMiddleware( ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            this.logger = logger;
            this.environment = environment;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                var response = new ApiExceptionResponse(StatusCodes.Status404NotFound, ex.Message);

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
            catch (ForbidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access forbidden");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var response = environment.IsDevelopment() ? new ApiExceptionResponse(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace?.ToString()) : new ApiExceptionResponse(StatusCodes.Status500InternalServerError);

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);

            }
            
      }
    }
}
