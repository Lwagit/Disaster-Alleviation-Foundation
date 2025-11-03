using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using FluentAssertions;

namespace GiftofGivers.Tests.Integration
{
    // Make sure the generic type is your Program class from GiftofGivers_web
    public class AppIntegrationTests : IClassFixture<WebApplicationFactory<GiftofGivers_web.Program>>
    {
        private readonly WebApplicationFactory<GiftofGivers_web.Program> _factory;

        public AppIntegrationTests(WebApplicationFactory<GiftofGivers_web.Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_HomePage_ReturnsSuccess()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Welcome"); // adjust based on your home page text
        }

        [Fact]
        public async Task Get_LoginPage_ReturnsSuccess()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Account/Login");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Login"); // adjust based on your login page text
        }

        [Fact]
        public async Task Get_RegisterPage_ReturnsSuccess()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/Account/Register");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            content.Should().Contain("Register"); // adjust based on your register page text
        }
    }
}
