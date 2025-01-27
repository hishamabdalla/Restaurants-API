namespace Restaurants.Application.User
{
    public record CurrentUser(string Id,string Email,IEnumerable<string> UserRoles, string? Nationality,
    DateOnly? DateOfBirth)
    {
       
         public bool InInRole(string RoleName) =>UserRoles.Contains(RoleName);

            
    }
}
