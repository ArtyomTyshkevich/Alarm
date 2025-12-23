using Alarm.Maui.Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Alarm.Maui;

namespace Alarm.Maui.Platforms.Android
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var alarmId = Intent?.GetStringExtra("alarmId");

            if (!string.IsNullOrEmpty(alarmId))
            {
                AlarmNavigationService.PendingAlarmId = alarmId;
            }
        }
    }
}
