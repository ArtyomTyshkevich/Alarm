using Alarm.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Alarm.Infrastructure.Data
{
    public class AlarmLocalDbContext : DbContext
    {
        public AlarmLocalDbContext()
        {
        }

        public DbSet<AlarmUnit> Alarm { get; set; }
        public DbSet<AlarmMode> AlarmModes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "alarm.db");

            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }

}
