using Microsoft.EntityFrameworkCore;
using GiftofGivers_web.Models;

namespace GiftofGivers_web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<Incident> Incidents { get; set; }
    }
}
