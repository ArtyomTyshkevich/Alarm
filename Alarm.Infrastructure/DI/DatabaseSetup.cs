using Alarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Alarm.Infrastructure.DI
{
    public static class DatabaseSetup
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "alarm.db");

            services.AddDbContext<AlarmLocalDbContext>();

            return services;
        }
    }
}
