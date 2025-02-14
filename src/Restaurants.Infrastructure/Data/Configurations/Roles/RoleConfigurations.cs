using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurants.Domain.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Data.Configurations.NewFolder
{
    public class RoleConfigurations : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "ade9b91e-8cdc-4da9-8541-c1d73453223d",
                    Name = UserRole.Admin,
                    NormalizedName = UserRole.Admin.ToUpper()
                },
                new IdentityRole
                {
                    Id = "57e3ba63-4d22-4e02-8b92-9be98dda6b95",
                    Name = UserRole.User,
                    NormalizedName = UserRole.User.ToUpper()
                },
                new IdentityRole
                {
                    Id = "62c3d2b0-e827-4c68-9df8-acee864ab4b6",
                    Name = UserRole.Owner,
                    NormalizedName = UserRole.Owner.ToUpper()
                }
            );
        }
    }
}
