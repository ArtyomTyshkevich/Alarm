using Alarm.Core.Models;
using Alarm.Infrastructure.Repositories;

namespace Alarm.Application.Interfaces
{
    public interface IAlarmUnitRepository
    {
        Task AddAsync(AlarmUnit alarm);
        Task DeleteAsync(Guid id);
        Task<List<AlarmUnit>> GetAllAsync();
        Task<AlarmUnit> GetByIdAsync(Guid id);
        Task SaveChangesAsync();
        Task UpdateAsync(AlarmUnit alarm);
    }
}
