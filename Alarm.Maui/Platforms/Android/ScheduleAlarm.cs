using Alarm.Core.Enums;
using Alarm.Core.Models;
using Alarm.Infrastructure.Data;
using Alarm.Maui.SistemServises.Android;
using Android.App;
using Android.Content;
using Microsoft.EntityFrameworkCore;

namespace Alarm.Maui;

public class AlarmScheduler : IAlarmScheduler
{
    private readonly AlarmLocalDbContext _db;

    public AlarmScheduler(AlarmLocalDbContext db)
    {
        _db = db;
    }

    public async Task ScheduleAlarm(Guid alarmId)
    {
        // Берём будильник из базы
        var alarm = await _db.Alarm.FirstOrDefaultAsync(x => x.Id == alarmId);

        if (alarm == null || alarm.Status != AlarmStatus.On)
            return;

        var context = global::Android.App.Application.Context;

        var intent = new Intent(context, typeof(AlarmReceiver));
        intent.PutExtra("alarmId", alarm.Id.ToString());

        var pendingIntent = PendingIntent.GetBroadcast(
            context,
            alarm.Id.GetHashCode(),
            intent,
            PendingIntentFlags.UpdateCurrent | PendingIntentFlags.Immutable
        );

        var alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService)!;

        var triggerTime = CalculateNextTrigger(alarm);

        alarmManager.SetExactAndAllowWhileIdle(
            AlarmType.RtcWakeup,
            new DateTimeOffset(triggerTime).ToUnixTimeMilliseconds(),
            pendingIntent
        );
    }

    private DateTime CalculateNextTrigger(AlarmUnit alarm)
    {
        var now = DateTime.Now;
        var todayTrigger = DateTime.Today.Add(alarm.Time);

        if (alarm.DaysOfWeek == null || alarm.DaysOfWeek.Count == 0)
            return todayTrigger > now ? todayTrigger : todayTrigger.AddDays(1);

        for (int i = 0; i < 7; i++)
        {
            var candidate = todayTrigger.AddDays(i);
            if (alarm.DaysOfWeek.Contains(candidate.DayOfWeek) && candidate > now)
                return candidate;
        }

        return todayTrigger.AddDays(7);
    }
}
