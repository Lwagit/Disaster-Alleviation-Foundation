using GiftofGivers_web.Data;
using GiftofGivers_web.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;

namespace GiftofGivers.Tests.Services
{
    public class IncidentServiceTests
    {
        private AppDbContext CreateContext(string name)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public async Task ReportIncident_AddsToDatabase()
        {
            var db = CreateContext(nameof(ReportIncident_AddsToDatabase));
            var inc = new Incident { DisasterType = "Flood", Location = "Town A", Description = "Severe flooding" };
            db.Incidents.Add(inc);
            await db.SaveChangesAsync();

            var saved = await db.Incidents.FirstOrDefaultAsync(i => i.DisasterType == "Flood");
            saved.Should().NotBeNull();
            saved.Location.Should().Be("Town A");
        }
    }
}
