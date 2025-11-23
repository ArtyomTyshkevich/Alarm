
using Alarm.Infrastructure.DI;

namespace Alarm.Maui.DI
{
    public static class BusinessLogicRegister
    {
        public static void AddBusinessLogic(this IServiceCollection services)
        {
            services.AddDatabase();
        }
    }
}
