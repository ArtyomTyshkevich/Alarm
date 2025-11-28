using Alarm.Application.Interfaces.Repositories;
using Alarm.Application.Interfaces.Repositories.UnitOfWork;
using Alarm.Application.Interfaces.Services;
using Alarm.Core.Services;
using Alarm.Infrastructure.DI;
using Alarm.Infrastructure.Repositories;
using Alarm.Infrastructure.Repositories.UnitOfWork;
using Alarm.Maui.ViewModels;
using Alarm.Maui.Views;

namespace Alarm.Maui.DI
{
    public static class BusinessLogicRegister
    {
        public static void AddBusinessLogic(this IServiceCollection services)
        {
            services.AddDatabase();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IAlarmModeRepository, AlarmModeRepository>();
            services.AddSingleton<IAlarmUnitRepository, AlarmUnitRepository>();
            services.AddSingleton<IAlarmUnitService, AlarmUnitService>();
            services.AddSingleton<IAlarmModeService, AlarmModeService>();
            services.AddSingleton<MainPageViewModel>();
            services.AddTransient<AddAlarmPageViewModel>();
            services.AddSingleton<MainPage>();
            services.AddTransient<AddAlarmPage>();
            services.AddSingleton<UpdateAlarmPage>();
            services.AddTransient<UpdateAlarmPageViewModel>();
        }
    }
}
