using Alarm.Core.Models;

namespace Alarm.Application.Interfaces.Services
{
    public interface IAlarmScheduler
    {
        Task ScheduleAlarm(Guid alarmId);
        Task CancelAlarm(Guid alarmId);
    }
}
