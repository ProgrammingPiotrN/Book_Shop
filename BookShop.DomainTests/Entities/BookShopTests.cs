﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace BookShop.Domain.Entities.Tests
{
    [TestClass()]
    public class BookShopTests
    {
        [TestMethod()]
        public void CodeUrlTest()
        {
            var bookShop = new BookShop();
            bookShop.Name = "Test jednostkowy";
            bookShop.CodeUrl();

            bookShop.EncodedName.Should().Be("test-jednostkowy");
        }

        [TestMethod()]
        public void CodeUrl_IsNull()
        {
            var bookShop = new BookShop();
            Action action = () => bookShop.CodeUrl();

            action.Invoking(p => p.Invoke())
                .Should().Throw<NullReferenceException>();

        }
    }
}