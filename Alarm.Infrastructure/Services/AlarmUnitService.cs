using Alarm.Application.Interfaces.Repositories.UnitOfWork;
using Alarm.Application.Interfaces.Services;
using Alarm.Core.Models;

namespace Alarm.Core.Services
{
    public class AlarmUnitService : IAlarmUnitService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlarmUnitService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AlarmUnit>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.AlarmUnits.GetAllAsync(cancellationToken);
        }

        public async Task<AlarmUnit?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _unitOfWork.AlarmUnits.GetByIdAsync(id, cancellationToken);
        }

        public async Task AddAsync(AlarmUnit alarm, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.AlarmUnits.AddAsync(alarm, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(AlarmUnit alarm, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.AlarmUnits.UpdateAsync(alarm, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.AlarmUnits.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
