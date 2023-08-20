using Xunit;
using BookShop.Application.BookShopService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Domain.Entities;
using Moq;
using BookShop.Application.ApplicationUser;
using BookShop.Application.BookShop;
using BookShop.Domain.Interfaces;
using FluentAssertions;
using MediatR;


namespace BookShop.Application.BookShopService.Commands.Tests
{
    
    public class CreateBookShopServiceCommandHandlerTests
    {
        [Fact()]
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

        [Fact()]
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

        [Fact()]
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

        [Fact()]
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