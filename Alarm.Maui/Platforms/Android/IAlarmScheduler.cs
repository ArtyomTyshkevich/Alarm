
namespace Alarm.Maui
{
    public interface IAlarmScheduler
    {
        Task ScheduleAlarm(Guid alarmId);
    }
}