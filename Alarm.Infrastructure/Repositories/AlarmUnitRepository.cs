using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alarm.Application.Interfaces.Repositories;
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


        public async Task<List<AlarmUnit>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Alarm
                                   .Include(a => a.Mode)
                                   .ToListAsync(cancellationToken);
        }
        public async Task<AlarmUnit?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Alarm
                                   .Include(a => a.Mode) 
                                   .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task AddAsync(AlarmUnit alarm, CancellationToken cancellationToken = default)
        {
            await _dbContext.Alarm.AddAsync(alarm, cancellationToken);
        }

        public Task UpdateAsync(AlarmUnit alarm, CancellationToken cancellationToken = default)
        {
            _dbContext.Alarm.Update(alarm);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var alarm = await _dbContext.Alarm.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
            if (alarm != null)
            {
                _dbContext.Alarm.Remove(alarm);
            }
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
