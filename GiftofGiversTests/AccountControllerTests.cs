using System.Linq;
using System.Threading.Tasks;
using Xunit;
using GiftofGivers_web.Controllers;
using GiftofGivers_web.Data;
using GiftofGivers_web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace GiftofGivers.Tests.Controllers
{
    public class AccountControllerTests
    {
        private AppDbContext CreateInMemoryContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public void Register_Post_CreatesUserAndRedirects()
        {
            // Arrange
            var db = CreateInMemoryContext(nameof(Register_Post_CreatesUserAndRedirects));
            var hasher = new PasswordHasher<User>();
            var controller = new AccountController(db, hasher);

            // Act
            var result = controller.Register("testuser", "Password123!") as RedirectToActionResult;

            // Assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("Login"); // matches controller

            var u = db.Users.FirstOrDefault(x => x.Username == "testuser");
            u.Should().NotBeNull();
            u.Username.Should().Be("testuser");
        }

        [Fact]
        public async Task Login_Post_WithValidCredentials_RedirectsToHomeIndex()
        {
            // Arrange
            var db = CreateInMemoryContext(nameof(Login_Post_WithValidCredentials_RedirectsToHomeIndex));
            var hasher = new PasswordHasher<User>();
            var controller = new AccountController(db, hasher);

            var user = new User { Username = "testuser" };
            user.PasswordHash = hasher.HashPassword(user, "Password123!");
            db.Users.Add(user);
            await db.SaveChangesAsync();

            // Act
            var result = await controller.Login("testuser", "Password123!") as RedirectToActionResult;

            // Assert
            result.Should().NotBeNull();
            result.ActionName.Should().Be("Index");
            result.ControllerName.Should().Be("Home");
        }

        [Fact]
        public async Task Login_Post_WithInvalidCredentials_ReturnsView()
        {
            // Arrange
            var db = CreateInMemoryContext(nameof(Login_Post_WithInvalidCredentials_ReturnsView));
            var hasher = new PasswordHasher<User>();
            var controller = new AccountController(db, hasher);

            // Act
            var result = await controller.Login("nouser", "wrongpass") as ViewResult;

            // Assert
            result.Should().NotBeNull();
            result.ViewData["Error"].Should().Be("Invalid username or password.");
        }
    }
}
