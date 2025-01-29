using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Restaurants.API.Middleware;
using Restaurants.Application.Dishes.DTOs;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.DTOs;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Application.Restaurants.RestaurantDtos;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces.Repositories.Interfaces;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requierments;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Data.Contexts;
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
        services.AddIdentityService();
        services.AddAuthenticationService();
        return services;
    }

    private static IServiceCollection AddBuildInServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddControllers();
        return services;

    }
    private static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { 
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },

                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },

                     new List<string>()
                }
            });
        });

    
        return services;
    }

    private static IServiceCollection AddUserDefindService(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork,UnitOfWork>();
        services.AddScoped<IUserContext,UserContext>();
        services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();

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
        host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration)

        );
    }
    private static IServiceCollection AddIdentityService(this IServiceCollection services)
    {
        services.AddIdentityApiEndpoints<AppUser>()
             .AddRoles<IdentityRole>()
             .AddClaimsPrincipalFactory<RestaruantUserClaimsPrincipalFactory>()
             .AddEntityFrameworkStores<RestaurantsDbContext>();
             
        services.AddAuthorization(options=>
{
            options.AddPolicy(PolicyNames.HasNationality, 
                policy => policy.RequireClaim(AppClaimTypes.Nationality,"Egyptian"));
            options.AddPolicy(PolicyNames.AtLeast20,
                builder => builder.AddRequirements(new MinimumAgeRequirement(20)));


        });

        services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();


        return services;
        
    }

    private static IServiceCollection AddAuthenticationService(this IServiceCollection services)
    {
        services.AddAuthentication();
        return services;
    }



}
