using Microsoft.EntityFrameworkCore;
using Restaurants.API;
using Restaurants.API.Helper;
using Restaurants.Infrastructure.Data.Contexts;
using Restaurants.Infrastructure.Data.Seeder.RestaurantsSeeder;
using Restaurants.Infrastructure.Helper;

var builder = WebApplication.CreateBuilder(args);

    builder.Services.Dependency(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    await app.ConfigureMiddlewareAsync();

    app.Run();
        