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

namespace Restaurants.Application.Users.Commands.AssignUserRole
{
    public class AssignUserRoleCommandHandler : IRequestHandler<AssignUserRoleCommand>
    {
        private readonly ILogger<AssignUserRoleCommandHandler> logger;
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Assigning user role: {@Request}", request);

            var user = await userManager.FindByEmailAsync(request.UserEmail)
            ?? throw new NotFoundException(nameof(User), request.UserEmail);

            var role = await roleManager.FindByNameAsync(request.RoleName)
                ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);


            await userManager.AddToRoleAsync(user, role.Name!);


        }
    }
}
