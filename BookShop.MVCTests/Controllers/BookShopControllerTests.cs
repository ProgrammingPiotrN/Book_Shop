using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using MediatR;
using FluentAssertions;
using System.Net;
using BookShop.Application.BookShop;
using BookShop.Application.BookShop.Queries.GetAllBookShops;

namespace BookShop.MVC.Controllers.Tests
{
    public class BookShopControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BookShopControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact()]
        public async Task Index_ReturnsViewWithExpectedData_ForExistingBookshops()
        {
            var bookShops = new List<BookShopDto>()
            {
                new BookShopDto()
                {
                    Name = "BookShop 1",
                },

                new BookShopDto()
                {
                    Name = "BookShop 2",
                },

                new BookShopDto()
                {
                    Name = "BookShop 3",
                },
            };

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllBookShopsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookShops);

            var client = _factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services => services.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            var response = await client.GetAsync("/BookShop/Index");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<h1>Book Shops</h1>")
                .And.Contain("BookShop 1")
                .And.Contain("BookShop 2")
                .And.Contain("BookShop 3");
        }

        [Fact()]
        public async Task Index_ReturnsEmptyView_WhenNoBookShopsExist()
        {
            // arrange

            var bookShops = new List<BookShopDto>();

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(m => m.Send(It.IsAny<GetAllBookShopsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(bookShops);

            var client = _factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureServices(services => services.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            // act

            var response = await client.GetAsync("/BookShop/Index");

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().NotContain("div class=\"card m-3\"");
        }
    }
}