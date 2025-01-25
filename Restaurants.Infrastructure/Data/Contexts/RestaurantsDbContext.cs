using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Data.Configurations;
using System.Reflection;
namespace Restaurants.Infrastructure.Data.Contexts;

public class RestaurantsDbContext :IdentityDbContext<AppUser>
{
    public RestaurantsDbContext(DbContextOptions<RestaurantsDbContext> options): base(options)
    {


    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // modelBuilder.ApplyConfiguration(new RestaurantsConfigurations());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }


    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Dish> Dishes { get; set; }
}
