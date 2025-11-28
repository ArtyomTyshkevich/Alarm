using Alarm.Core.Models;

namespace Alarm.Application.Interfaces.Repositories
{
    public interface IAlarmModeRepository
    {
        Task AddAsync(AlarmMode mode, CancellationToken cancellationToken);
        Task DeleteAsync(string name, CancellationToken cancellationToken);
        Task<List<AlarmMode>> GetAllAsync(CancellationToken cancellationToken);
        Task<AlarmMode?> GetByNameAsync(string name, CancellationToken cancellationToken);
    }
}
