using Alarm.Maui.Views;

namespace Alarm.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddAlarmPage), typeof(AddAlarmPage));
            Routing.RegisterRoute(nameof(UpdateAlarmPage), typeof(UpdateAlarmPage));
        }
    }
}
