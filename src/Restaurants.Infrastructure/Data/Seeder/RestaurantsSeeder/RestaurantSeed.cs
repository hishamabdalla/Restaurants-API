using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Data.Seeder.RestaurantsSeeder
{
    public static class RestaurantSeed
    {
        public async static Task Seed(RestaurantsDbContext _context)
        {
            if (!_context.Restaurants.Any())
            {
                var data = File.ReadAllText(@"..\Restaurants.Infrastructure\Data\Seeder\DataSeed\Restaurant.json");

                var restaurants = JsonSerializer.Deserialize<List<Restaurant>>(data);

                if (restaurants is not null && restaurants.Count() > 0)
                {
                   await _context.Restaurants.AddRangeAsync(restaurants);
                  await _context.SaveChangesAsync();
                }
            }
        }
    }
}
