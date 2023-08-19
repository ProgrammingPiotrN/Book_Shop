using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookShop.Application.ApplicationUser;
using Moq;
using BookShop.Domain.Interfaces;
using FluentAssertions;
using MediatR;

namespace BookShop.Application.BookShopService.Commands.Tests
{
    [TestClass()]
    public class CreateBookShopServiceCommandHandlerTests
    {
        [TestMethod()]
        public async Task CreatesBookShopServiceCommandHandler_WhenUserIsAuthorized()
        {
            var bookShop = new Domain.Entities.BookShop()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateBookShopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                BookShopEncodedName = "bookshop1"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));


            var bookShopRepositoryMock = new Mock<IBookShopRepository>();
            bookShopRepositoryMock.Setup(c => c.GetByEncodedName(command.BookShopEncodedName))
                .ReturnsAsync(bookShop);

            var bookShopServiceRepositoryMock = new Mock<IBookShopServiceRepository>();

            var handler = new CreateBookShopServiceCommandHandler(userContextMock.Object, bookShopRepositoryMock.Object,
                bookShopServiceRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);
            result.Should().Be(Unit.Value);
            bookShopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.BookShopService>()), Times.Once);
        }

        [TestMethod()]
        public async Task CreatesBookShopServiceCommandHandler_WhenUserIsModerator()
        {
            var bookShop = new Domain.Entities.BookShop()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateBookShopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                BookShopEncodedName = "bookshop"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("2", "test@test.com", new[] { "Moderator" }));

            var bookShopRepositoryMock = new Mock<IBookShopRepository>();
            bookShopRepositoryMock.Setup(c => c.GetByEncodedName(command.BookShopEncodedName))
                .ReturnsAsync(bookShop);

            var bookShopServiceRepositoryMock = new Mock<IBookShopServiceRepository>();

            var handler = new CreateBookShopServiceCommandHandler(userContextMock.Object, bookShopRepositoryMock.Object,
                bookShopServiceRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().Be(Unit.Value);
            bookShopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.BookShopService>()), Times.Once);
        }

        [TestMethod()]
        public async Task CreatesBookShopServiceCommandHandler_WhenUserIsNotAuthorized()
        {
            var bookShop = new Domain.Entities.BookShop()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateBookShopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                BookShopEncodedName = "bookshop"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("2", "test@test.com", new[] { "User" }));

            var bookShopRepositoryMock = new Mock<IBookShopRepository>();
            bookShopRepositoryMock.Setup(c => c.GetByEncodedName(command.BookShopEncodedName))
                .ReturnsAsync(bookShop);

            var bookShopServiceRepositoryMock = new Mock<IBookShopServiceRepository>();

            var handler = new CreateBookShopServiceCommandHandler(userContextMock.Object, bookShopRepositoryMock.Object,
                bookShopServiceRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);
            result.Should().Be(Unit.Value);
            bookShopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.BookShopService>()), Times.Never);
        }

        [TestMethod()]
        public async Task CreatesBookShopServiceCommandHandler_WhenUserIsNotAuthenticated()
        {
            var bookShop = new Domain.Entities.BookShop()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateBookShopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                BookShopEncodedName = "bookshop"
            };

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns((CurrentUser?)null);

            var bookShopRepositoryMock = new Mock<IBookShopRepository>();
            bookShopRepositoryMock.Setup(c => c.GetByEncodedName(command.BookShopEncodedName))
                .ReturnsAsync(bookShop);

            var bookShopServiceRepositoryMock = new Mock<IBookShopServiceRepository>();

            var handler = new CreateBookShopServiceCommandHandler(userContextMock.Object, bookShopRepositoryMock.Object,
                bookShopServiceRepositoryMock.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().Be(Unit.Value);
            bookShopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.BookShopService>()), Times.Never);

        }
    }
}