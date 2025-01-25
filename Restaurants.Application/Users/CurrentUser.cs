namespace Restaurants.Application.User
{
    public record CurrentUser(string Id,string Email,IEnumerable<string> UserRoles)
    {
       
         public bool InInRole(string RoleName) =>UserRoles.Contains(RoleName);

            
    }
}
