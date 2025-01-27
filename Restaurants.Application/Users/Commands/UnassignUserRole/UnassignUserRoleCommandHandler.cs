using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Exceptions;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands.UnassignUserRole
{
    public class UnassignUserRoleCommandHandler(ILogger<UnassignUserRoleCommandHandler> logger,
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<UnassignUserRoleCommand>
    {

        public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Unassigning user role: {@Request}", request);

            var user = await userManager.FindByEmailAsync(request.UserEmail)
                ?? throw new NotFoundException(nameof(User), request.UserEmail);

            var role = await roleManager.FindByNameAsync(request.RoleName)
                ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

            await userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}