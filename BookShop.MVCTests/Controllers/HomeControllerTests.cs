using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using FluentAssertions;
using System.Net;

namespace BookShop.MVC.Controllers.Tests
{
    public class HomeControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public HomeControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact()]
        public async Task ReturnsViewWithRenderModel_About()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/Home/About");

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<h1>BookShop application</h1>")
                .And.Contain("<div class=\"alert alert-primary\">Some description</div>")
                .And.Contain("<li>book</li>")
                .And.Contain("<li>liblary</li>")
                .And.Contain("<li>shop</li>");
        }
    }
}