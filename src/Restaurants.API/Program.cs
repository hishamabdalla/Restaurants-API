using Microsoft.EntityFrameworkCore;
using Restaurants.API;
using Restaurants.API.Helper;
using Restaurants.Infrastructure.Data.Contexts;
using Restaurants.Infrastructure.Data.Seeder.RestaurantsSeeder;
using Restaurants.Infrastructure.Helper;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

    builder.Services.Dependency(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Host.AddSerilogServices();
    var app = builder.Build();

 await app.ConfigureMiddlewareAsync();

    app.Run();

public partial class Program { }
        