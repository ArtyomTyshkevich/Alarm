using Alarm.Core.Models;

namespace Alarm.Application.Interfaces.Services
{
    public interface IAlarmUnitService
    {
        Task<IEnumerable<AlarmUnit>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AlarmUnit?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(AlarmUnit alarm, CancellationToken cancellationToken = default);
        Task UpdateAsync(AlarmUnit alarm, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
