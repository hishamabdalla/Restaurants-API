using Xunit;
using Restaurants.Application.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Constant;
using FluentAssertions;

namespace Restaurants.Application.User.Tests
{
    public class GetCurrentUserTests
    {
        [Theory]
        [InlineData(UserRole.Admin)]
        [InlineData(UserRole.User)]
        public void InRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            //arrange
            var currentUser=new CurrentUser("1", "test@test.com", [UserRole.Admin,UserRole.User],null,null);

            //act

            var isInRole=currentUser.InRole(roleName);

            //assert
            isInRole.Should().BeTrue();

        }

        [Fact()]
        public void InRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            //arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRole.Admin, UserRole.User], null, null);

            //act

            var isInRole = currentUser.InRole(UserRole.Owner);

            //assert
            isInRole.Should().BeFalse();

        }

        [Fact()]
        public void InRole_WithNoMatchingRoleCase_ShouldReturnFalse()
        {
            //arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRole.Admin, UserRole.User], null, null);

            //act

            var isInRole = currentUser.InRole(UserRole.Admin.ToLower());

            //assert
            isInRole.Should().BeFalse();

        }
    }
}