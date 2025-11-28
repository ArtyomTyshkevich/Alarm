using Alarm.Application.Interfaces.Repositories;
using Alarm.Infrastructure.Data;
using Alarm.Application.Interfaces.Repositories.UnitOfWork;

namespace Alarm.Infrastructure.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AlarmLocalDbContext _dbContext;

        private AlarmUnitRepository? _alarmUnitRepository;
        private AlarmModeRepository? _alarmModeRepository;

        public UnitOfWork(AlarmLocalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAlarmUnitRepository AlarmUnits =>
            _alarmUnitRepository ??= new AlarmUnitRepository(_dbContext);

        public IAlarmModeRepository AlarmModes =>
            _alarmModeRepository ??= new AlarmModeRepository(_dbContext);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
