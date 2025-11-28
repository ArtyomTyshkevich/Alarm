
namespace Alarm.Application.Interfaces.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAlarmModeRepository AlarmModes { get; }
        IAlarmUnitRepository AlarmUnits { get; }

        void Dispose();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
