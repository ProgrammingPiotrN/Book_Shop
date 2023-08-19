using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using BookShop.Application.ApplicationUser;
using Moq;
using BookShop.Application.BookShop;
using FluentAssertions;
using BookShop.Domain.Entities;

namespace BookShop.Application.Mappings.Tests
{
    [TestClass()]
    public class BookShopMappingProfileTests
    {
        [TestMethod()]
        public void MappingProfile_MapCarWorkshopDtoToCarWorkshop()
        {
            var userContextMock = new Mock<IUserContext>();
            userContextMock
                .Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@example.com", new[] { "Moderator" }));


            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new BookShopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var dto = new BookShopDto
            {
                City = "Kalisz",
                NumberPhone = "357897126",
                PostalCode = "62800",
                Street = "Nowa"
            };

            var result = mapper.Map<Domain.Entities.BookShop>(dto);

            result.Should().NotBeNull();
            result.ContactDetails.Should().NotBeNull();
            result.ContactDetails.City.Should().Be(dto.City);
            result.ContactDetails.NumberPhone.Should().Be(dto.NumberPhone);
            result.ContactDetails.PostalCode.Should().Be(dto.PostalCode);
            result.ContactDetails.Street.Should().Be(dto.Street);
        }

        [TestMethod()]
        public void MappingProfile_MapCarWorkshopToCarWorkshopDto()
        {
            var userContextMock = new Mock<IUserContext>();
            userContextMock
                .Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@example.com", new[] { "Moderator" }));


            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new BookShopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var bookShop = new Domain.Entities.BookShop
            {
                Id = 1,
                CreatedById = "1",
                ContactDetails = new BookShopContactDetails
                {
                    City = "City",
                    NumberPhone = "123456789",
                    PostalCode = "12345",
                    Street = "Street"
                }
            };

            var result = mapper.Map<BookShopDto>(bookShop);

            result.Should().NotBeNull();

            result.IsEditable.Should().BeTrue();
            result.Street.Should().Be(bookShop.ContactDetails.Street);
            result.City.Should().Be(bookShop.ContactDetails.City);
            result.PostalCode.Should().Be(bookShop.ContactDetails.PostalCode);
            result.NumberPhone.Should().Be(bookShop.ContactDetails.NumberPhone);
        }
    }
}