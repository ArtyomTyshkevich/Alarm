using Alarm.Application.Interfaces;
using Alarm.Core.Models;
using Alarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Alarm.Infrastructure.Repositories
{
    public class AlarmUnitRepository : IAlarmUnitRepository
    {
        private readonly AlarmLocalDbContext _dbContext;

        public AlarmUnitRepository(AlarmLocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AlarmUnit>> GetAllAsync()
        {
            return await _dbContext.Alarm.ToListAsync();
        }

        public async Task<AlarmUnit> GetByIdAsync(Guid id)
        {
            return await _dbContext.Alarm.FirstOrDefaultAsync(a => a.Id == id);
        }

        public Task AddAsync(AlarmUnit alarm)
        {
            _dbContext.Alarm.Add(alarm);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(AlarmUnit alarm)
        {
            _dbContext.Alarm.Update(alarm);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var alarm = await _dbContext.Alarm.FirstOrDefaultAsync(a => a.Id == id);
            if (alarm != null)
            {
                _dbContext.Alarm.Remove(alarm);
            }
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
