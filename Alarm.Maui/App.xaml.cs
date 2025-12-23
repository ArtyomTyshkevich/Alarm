using Alarm.Infrastructure.Data;
using Alarm.Maui.Android;
using Microsoft.EntityFrameworkCore;


namespace Alarm.Maui
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        private readonly AlarmLocalDbContext _context;

        public App(AlarmLocalDbContext context)
        {
            InitializeComponent();

            _context = context;
            _context.Database.Migrate();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = new Window(new AppShell());

            // Отложенный переход к странице будильника после загрузки Shell
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    // Проверяем, есть ли ожидающий будильник
                    if (AlarmNavigationService.PendingAlarmId != null)
                    {
                        var alarmId = AlarmNavigationService.PendingAlarmId;
                        AlarmNavigationService.PendingAlarmId = null;

                        // Навигация через Shell
                        await Shell.Current.GoToAsync($"alarmringing?alarmId={alarmId}");
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Navigation error: {ex}");
                }
            });

            return window;
        }
    }
}
