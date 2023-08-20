using Xunit;
using BookShop.Application.BookShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace BookShop.Application.ApplicationUser.Tests
{
    
    public class CurrentUserTests
    {
        [Fact()]
        public void IsInRole_ReturnTrue()
        {
            var currentUser = new CurrentUser("1", "test@test.com", new List<string> { "Admin", "User" });
            var isInRole = currentUser.IsInRole("Admin");
            isInRole.Should().BeTrue();
        }

        [Fact()]
        public void IsInRole_ReturnFalse()
        {
            var currentUser = new CurrentUser("1", "test@test.com", new List<string> { "Admin", "User" });
            var isInRole = currentUser.IsInRole("Manager");
            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void IsInRole_ReturnMatchingCaseRoleFalse()
        {
            var currentUser = new CurrentUser("1", "test@test.com", new List<string> { "Admin", "User" });
            var isInRole = currentUser.IsInRole("admin");
            isInRole.Should().BeFalse();
        }
    }
}