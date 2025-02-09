using Xunit;
using Restaurants.Application.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Restaurants.Domain.Constant;
using FluentAssertions;
using static System.Net.Mime.MediaTypeNames;

namespace Restaurants.Application.User.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            //arrange 
            var dateOfBirth = new DateOnly(2000, 1, 1);
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier,"1"),
                new(ClaimTypes.Email,"test@test.com"),
                new(ClaimTypes.Role,UserRole.Admin),
                new(ClaimTypes.Role,UserRole.User),
                new("Nationality","German"),
                new("DateOfBirth",dateOfBirth.ToString("yyyy-MM-dd"))

            };

            var user=new ClaimsPrincipal(new ClaimsIdentity(claims,"Test"));
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User=user
                
            });
            var userContext = new UserContext(httpContextAccessorMock.Object);
 
            //act 
            var currentUser = userContext.GetCurrentUser();

            //asset

            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.UserRoles.Should().ContainInOrder(UserRole.Admin,UserRole.User);
            currentUser.Nationality.Should().Be("German");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);

        }

        [Fact]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowInvalidOperations()
        {
            //arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

            var userContext=new UserContext(httpContextAccessorMock.Object);

            //act
            Action act = () => userContext.GetCurrentUser();

            //assert
            act.Should().Throw<InvalidDataException>().
                WithMessage("User not found");
        }
    }
}