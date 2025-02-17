﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.API.Middleware;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Data.Contexts;
using Restaurants.Infrastructure.Data.Seeder.RestaurantsSeeder;
using Restaurants.Infrastructure.Data.Seeder.UserSeeder;
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
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            await context.Database.MigrateAsync();
            await RestaurantSeed.Seed(context);
            await UserSeed.Seed(userManager);

            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.MapGroup("/api/Accounts")
                .WithTags("Accounts")
                .MapIdentityApi<AppUser>();

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            return app;
        }
    }
}
