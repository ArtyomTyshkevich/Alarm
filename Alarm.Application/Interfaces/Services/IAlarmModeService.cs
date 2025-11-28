using Alarm.Core.Models;

namespace Alarm.Application.Interfaces.Services
{
    public interface IAlarmModeService
    {
        Task AddAsync(AlarmMode mode, CancellationToken cancellationToken = default);
        Task DeleteAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<AlarmMode>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
