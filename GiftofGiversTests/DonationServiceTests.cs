using System.Linq;
using System.Threading.Tasks;
using GiftofGivers_web.Data;
using GiftofGivers_web.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;

namespace GiftofGivers.Tests.Services
{
    public class DonationServiceTests
    {
        private AppDbContext CreateContext(string name)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddDonation_SavesDonation()
        {
            var db = CreateContext(nameof(AddDonation_SavesDonation));
            var donation = new Donation
            {
                ResourceType = "Food",
                Quantity = 10,
                DonorName = "Alice",
                DonorContact = "alice@example.com"
            };

            db.Donations.Add(donation);
            await db.SaveChangesAsync();

            var list = await db.Donations.ToListAsync();
            list.Should().HaveCount(1);
            list[0].ResourceType.Should().Be("Food");
        }
    }
}
