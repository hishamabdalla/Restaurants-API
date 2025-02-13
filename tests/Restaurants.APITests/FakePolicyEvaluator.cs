using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.APITests
{
    internal class FakePolicyEvaluator : IPolicyEvaluator
    {
        public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            var claimPrincipal = new ClaimsPrincipal();
            var claimsIdentity = new ClaimsIdentity();


            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, "TestUser"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "1"));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
            claimPrincipal.AddIdentity(claimsIdentity);

            var ticket = new AuthenticationTicket(claimPrincipal, "TestScheme");

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);


            
        }

        public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object? resource)
        {
            var result = PolicyAuthorizationResult.Success();
            
            return Task.FromResult(result);

        }
    }
}
