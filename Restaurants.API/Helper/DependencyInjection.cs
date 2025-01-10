using System.Runtime.CompilerServices;

namespace Restaurants.API.Helper
{
    public static class DependencyInjection
    {
        public static IServiceCollection Dependency(this IServiceCollection services,IConfiguration configuration )
        {
            services.AddBuildInServices();
            services.AddSwaggerService();
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

    }
}
