using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Application.User;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Users.Commands.UpdateUsers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand,bool>
    {
        private readonly ILogger<UpdateUserCommandHandler> logger;
        private readonly IUserContext userContext;
        private readonly IUserStore<AppUser> userStore;

        public UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger,IUserContext userContext,IUserStore<AppUser> userStore)
        {
            this.logger = logger;
            this.userContext = userContext;
            this.userStore = userStore;
        }
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            logger.LogInformation("Updating user {@UserId}, With {@Request}",user?.Id,request);

            var dbUser= await userStore.FindByIdAsync(user!.Id, cancellationToken);

            if (dbUser == null)
            {
                //throw new NotFoundException("User not found");
                return false;
            }
            dbUser.DateOfBirth = request.DateOfBirth;
            dbUser.Nationality=request.Nationality;

             await userStore.UpdateAsync(dbUser, cancellationToken);
            return true;
        }
    }
}
