using Microsoft.EntityFrameworkCore;
using Restaurants.API;
using Restaurants.Infrastructure.Data.Contexts;
using Restaurants.Infrastructure.Data.Seeder.RestaurantsSeeder;
using Restaurants.Infrastructure.Helper;

var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();

    builder.Services.AddInfrastructure(builder.Configuration);

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add services to the container.
    builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();



    var app = builder.Build();

    using var scope=app.Services.CreateScope();
    var services = scope.ServiceProvider;
    
     var context=services.GetRequiredService<RestaurantsDbContext>();
     await context.Database.MigrateAsync();
     await RestaurantSeed.Seed(context);


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();


    app.MapControllers();

    app.Run();
        