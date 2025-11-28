using Alarm.Core.Models;
using Alarm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Alarm.Application.Interfaces.Repositories;

namespace Alarm.Infrastructure.Repositories
{
    public class AlarmModeRepository : IAlarmModeRepository
    {
        private readonly AlarmLocalDbContext _dbContext;

        public AlarmModeRepository(AlarmLocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AlarmMode>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.AlarmModes.ToListAsync(cancellationToken);
        }

        public async Task<AlarmMode?> GetByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.AlarmModes
                .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        }

        public async Task AddAsync(AlarmMode mode, CancellationToken cancellationToken)
        {
            await _dbContext.AlarmModes.AddAsync(mode, cancellationToken);
        }

        public async Task DeleteAsync(string name, CancellationToken cancellationToken)
        {
            var mode = await _dbContext.AlarmModes
                .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);

            if (mode != null)
                _dbContext.AlarmModes.Remove(mode);
        }
    }
}
