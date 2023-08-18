using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookShop.Application.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace BookShop.Application.ApplicationUser.Tests
{
    [TestClass()]
    public class CurrentUserTests
    {
        [TestMethod()]
        public void IsInRole_ReturnTrue()
        {
            var currentUser = new CurrentUser("1", "test@test.com", new List<string> { "Admin", "User" });
            var isInRole = currentUser.IsInRole("Admin");
            isInRole.Should().BeTrue();
        }

        [TestMethod()]
        public void IsInRole_ReturnFalse()
        {
            var currentUser = new CurrentUser("1", "test@test.com", new List<string> { "Admin", "User" });
            var isInRole = currentUser.IsInRole("Manager");
            isInRole.Should().BeFalse();
        }

        [TestMethod()]
        public void IsInRole_ReturnMatchingCaseRoleFalse()
        {
            var currentUser = new CurrentUser("1", "test@test.com", new List<string> { "Admin", "User" });
            var isInRole = currentUser.IsInRole("admin");
            isInRole.Should().BeFalse();
        }
    }
}