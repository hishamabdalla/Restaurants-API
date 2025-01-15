using Restaurants.Application.Mapping;
using Restaurants.Application.Services;
using Restaurants.Domain.Interfaces.Repositories.Interfaces;
using Restaurants.Domain.Interfaces.Services.Interfaces;
using Restaurants.Domain.Interfaces.UnitOfWork.Interface;
using Restaurants.Infrastructure.UnitOfWork;
using System.Runtime.CompilerServices;

namespace Restaurants.API.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection Dependency(this IServiceCollection services,IConfiguration configuration )
        {
            services.AddBuildInServices();
            services.AddSwaggerService();
            services.AddUserDefindService();
            services.AddAutoMapperService();
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
            services.AddScoped<IRestaurantService,RestaurantsService>();
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

    }
}
