using Microsoft.EntityFrameworkCore;
using Restaurants.Infrastructure.Data.Contexts;
using Restaurants.Infrastructure.Data.Seeder.RestaurantsSeeder;
using Serilog;

namespace Restaurants.API.Helper
{
    public static class ConfigureMiddleware
    {
        public async  static Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<RestaurantsDbContext>();
            await context.Database.MigrateAsync();
            await RestaurantSeed.Seed(context);


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;
        }
    }
}
