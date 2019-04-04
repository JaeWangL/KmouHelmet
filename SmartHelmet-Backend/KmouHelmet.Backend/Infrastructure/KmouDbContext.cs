using KmouHelmet.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace KmouHelmet.Backend.Infrastructure
{
    public class KmouDbContext : DbContext
    {
        public KmouDbContext(DbContextOptions<KmouDbContext> options) : base(options)
        {
        }

        public DbSet<DeviceModel> Devices { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
    }
}
