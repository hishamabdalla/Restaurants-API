using FluentValidation;
using FluentValidation.AspNetCore;
using Restaurants.Application.Mapping;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Application.Restaurants.RestaurantDtos;
using Restaurants.Domain.Interfaces.Repositories.Interfaces;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using Restaurants.Infrastructure.UnitOfWork;
using Serilog;
using Serilog.Events;
using System.Runtime.CompilerServices;

namespace Restaurants.API.Helper;

public static class DependencyInjection
{
    
    public static IServiceCollection Dependency(this IServiceCollection services,IConfiguration configuration )
    {
        
        services.AddBuildInServices();
        services.AddSwaggerService();
        services.AddUserDefindService();
        services.AddAutoMapperService();
        services.AddFluentValidationService();
        services.AddMediratorService();
        return services;
    }

    private static IServiceCollection AddBuildInServices(this IServiceCollection services)
    {
        services.AddControllers();
        return services;

    }
    private static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    private static IServiceCollection AddUserDefindService(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork,UnitOfWork>();
        
        return services;
    }

    private static IServiceCollection AddAutoMapperService(this IServiceCollection services)
    {
        //services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
        services.AddAutoMapper(m => m.AddProfile(new RestaurantProfile()));
        services.AddAutoMapper(m => m.AddProfile(new DishProfile()));
        return services;
    }

    private static IServiceCollection AddFluentValidationService(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssembly(typeof(CreateRestaurantCommandValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(UpdateRestaurantCommandValidator).Assembly);
             
        return services;
    }

    private static IServiceCollection AddMediratorService(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllRestaurantQueryHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetRestaurantByIdQueryHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateRestaurantCommandHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DeleteRestaurantCommandHandler).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UpdateRestaurantCommandHandler).Assembly));

        return services;
    }

    public static void AddSerilogServices(this ConfigureHostBuilder host)
    {
        host.UseSerilog((context,configuration)=>
            configuration
            .MinimumLevel.Override("Microsoft",LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore",LogEventLevel.Information)
            .WriteTo.Console(outputTemplate : "[{Timestamp:dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}| {NewLine}{Message:lj}{NewLine}{Exception}")
        );
    }


}
