
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Data.Configurations;

public class RestaurantsConfigurations : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.OwnsOne(R=>R.Address);
        builder.HasMany(R => R.Dishes)
            .WithOne()
            .HasForeignKey(D => D.RestaurantId);

        builder.HasOne(R => R.Owner)
            .WithMany(u=>u.Restaurants)
            .HasForeignKey(i=>i.OwnerId);
    }
}
