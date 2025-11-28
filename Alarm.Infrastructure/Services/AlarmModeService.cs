using Alarm.Application.Interfaces.Repositories.UnitOfWork;
using Alarm.Application.Interfaces.Services;
using Alarm.Core.Models;

namespace Alarm.Core.Services
{
    public class AlarmModeService : IAlarmModeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlarmModeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AlarmMode>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.AlarmModes.GetAllAsync(cancellationToken);
        }

        public async Task AddAsync(AlarmMode mode, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.AlarmModes.AddAsync(mode, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string name, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.AlarmModes.DeleteAsync(name, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
