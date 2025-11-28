using Alarm.Core.Enums;

namespace Alarm.Core.Models
{
    public class AlarmUnit
    {
        public Guid Id { get; set; }
        public TimeSpan Time { get; set; }
        public AlarmStatus Status { get; set; }
        public List<DayOfWeek> DaysOfWeek { get; set; }
        public AlarmMode Mode { get; set; }
    }
}
