using Android.Content;
using Alarm.Maui.Platforms.Android;


namespace Alarm.Maui.SistemServises.Android
{
    [BroadcastReceiver(Enabled = true, Exported = false)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var alarmId = intent.GetStringExtra("alarmId");

            var launchIntent = new Intent(context, typeof(MainActivity));
            launchIntent.AddFlags(ActivityFlags.NewTask);
            launchIntent.PutExtra("alarmId", alarmId);

            context.StartActivity(launchIntent);
        }
    }

}
