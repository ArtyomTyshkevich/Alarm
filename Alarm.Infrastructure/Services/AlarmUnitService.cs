using Alarm.Application.Interfaces.Services;
using Alarm.Application.Interfaces.Repositories.UnitOfWork;
using Alarm.Core.Models;
using Alarm.Core.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alarm.Core.Services
{
    public class AlarmUnitService : IAlarmUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAlarmScheduler? _scheduler;

        // Конструктор с опциональной платформоспецифичной зависимостью
        public AlarmUnitService(IUnitOfWork unitOfWork, IAlarmScheduler? scheduler = null)
        {
            _unitOfWork = unitOfWork;
            _scheduler = scheduler;
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

            // Планируем или отменяем будильник только если есть scheduler (т.е. Android)
            if (_scheduler != null)
            {
                if (alarm.Status == AlarmStatus.On)
                    await _scheduler.ScheduleAlarm(alarm.Id);
                else
                    await _scheduler.CancelAlarm(alarm.Id);
            }
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _unitOfWork.AlarmUnits.DeleteAsync(id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
