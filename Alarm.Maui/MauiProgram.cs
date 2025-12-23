using Alarm.Maui.DI;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Devices;

namespace Alarm.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Поставь здесь брейкпоинт, чтобы проверить платформу
            var platform = DeviceInfo.Platform; // <- брейкпоинт
            System.Diagnostics.Debug.WriteLine($"Current platform: {platform}");

#if ANDROID
            builder.Services.AddSingleton<IAlarmScheduler, AlarmScheduler>();
#endif

            builder.Services.AddBusinessLogic();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
