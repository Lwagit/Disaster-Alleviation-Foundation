using GiftofGivers_web.Data;
using GiftofGivers_web.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;

namespace GiftofGivers.Tests.Services
{
    public class VolunteerServiceTests
    {
        private AppDbContext CreateContext(string name)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task RegisterVolunteer_SavesVolunteer()
        {
            var db = CreateContext(nameof(RegisterVolunteer_SavesVolunteer));

            var v = new Volunteer
            {
                Name = "Bob",
                Email = "b@example.com",
                Phone = "123",
                TaskAssigned = "None" // required field fixed
            };

            db.Volunteers.Add(v);
            await db.SaveChangesAsync();

            var fetched = await db.Volunteers.FirstOrDefaultAsync(x => x.Email == "b@example.com");
            fetched.Should().NotBeNull();
            fetched.Name.Should().Be("Bob");
        }
    }
}
