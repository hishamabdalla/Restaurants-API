﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.User
{
    public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
    {
        public CurrentUser? CurrentUser()
        {
            var user = httpContextAccessor?.HttpContext?.User;

            if (user == null)
                throw new InvalidDataException("User not found");

            if (!user.Identity.IsAuthenticated || user.Identity == null)
                throw new UnauthorizedAccessException("User not authenticated");

            var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var email = user.FindFirstValue(ClaimTypes.Email)!;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value);

            return new CurrentUser(userId, email, roles);
        }

    }
}
