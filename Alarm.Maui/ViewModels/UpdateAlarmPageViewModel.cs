using Alarm.Application.Interfaces.Services;
using Alarm.Core.Enums;
using Alarm.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Alarm.Maui.ViewModels
{
    [QueryProperty(nameof(AlarmIdString), "AlarmIdString")]
    public partial class UpdateAlarmPageViewModel : ObservableObject
    {
        private readonly IAlarmUnitService _alarmService;
        private readonly IAlarmModeService _alarmModeService;

        // Строковое свойство для Shell query
        [ObservableProperty]
        private string alarmIdString;

        // Настоящий Guid
        private Guid AlarmId;

        [ObservableProperty]
        private TimeSpan time;

        public ObservableCollection<AlarmMode> Modes { get; } = new();

        [ObservableProperty]
        private AlarmMode selectedMode;

        [ObservableProperty]
        private bool status;

        [ObservableProperty] private bool monday;
        [ObservableProperty] private bool tuesday;
        [ObservableProperty] private bool wednesday;
        [ObservableProperty] private bool thursday;
        [ObservableProperty] private bool friday;
        [ObservableProperty] private bool saturday;
        [ObservableProperty] private bool sunday;

        public IAsyncRelayCommand LoadAlarmCommand { get; }
        public IRelayCommand UpdateCommand { get; }
        public IRelayCommand DeleteCommand { get; }

        public UpdateAlarmPageViewModel(IAlarmUnitService alarmService, IAlarmModeService alarmModeService)
        {
            _alarmService = alarmService;
            _alarmModeService = alarmModeService;

            LoadAlarmCommand = new AsyncRelayCommand(LoadAlarmAsync);
            UpdateCommand = new AsyncRelayCommand(UpdateAlarm);
            DeleteCommand = new AsyncRelayCommand(DeleteAlarm);

            LoadModesAsync().ConfigureAwait(false);
        }

        // Этот метод будет вызываться автоматически при установке AlarmIdString
        partial void OnAlarmIdStringChanged(string value)
        {
            if (Guid.TryParse(value, out Guid id))
            {
                AlarmId = id;
                Console.WriteLine($"Parsed GUID: {id}");
                // Загружаем данные будильника после парсинга ID
                LoadAlarmCommand.ExecuteAsync(null);
            }
            else
            {
                Console.WriteLine("Invalid GUID format");
            }
        }

        public async Task LoadAlarmAsync()
        {
            if (AlarmId == Guid.Empty) return;

            var alarm = await _alarmService.GetByIdAsync(AlarmId);
            if (alarm == null) return;

            Time = alarm.Time;
            Status = alarm.Status == AlarmStatus.On;

            Monday = alarm.DaysOfWeek.Contains(DayOfWeek.Monday);
            Tuesday = alarm.DaysOfWeek.Contains(DayOfWeek.Tuesday);
            Wednesday = alarm.DaysOfWeek.Contains(DayOfWeek.Wednesday);
            Thursday = alarm.DaysOfWeek.Contains(DayOfWeek.Thursday);
            Friday = alarm.DaysOfWeek.Contains(DayOfWeek.Friday);
            Saturday = alarm.DaysOfWeek.Contains(DayOfWeek.Saturday);
            Sunday = alarm.DaysOfWeek.Contains(DayOfWeek.Sunday);

            SelectedMode = Modes.FirstOrDefault(m => m.Name == alarm.Mode.Name) ?? Modes.FirstOrDefault();
        }

        private async Task LoadModesAsync()
        {
            var modes = await _alarmModeService.GetAllAsync();
            Modes.Clear();
            foreach (var mode in modes)
                Modes.Add(mode);
        }

        private async Task UpdateAlarm()
        {
            if (AlarmId == Guid.Empty) return;

            // Получаем уже отслеживаемую сущность
            var alarm = await _alarmService.GetByIdAsync(AlarmId);
            if (alarm == null) return;

            // Составляем список выбранных дней
            var selectedDays = new List<DayOfWeek>();
            if (Monday) selectedDays.Add(DayOfWeek.Monday);
            if (Tuesday) selectedDays.Add(DayOfWeek.Tuesday);
            if (Wednesday) selectedDays.Add(DayOfWeek.Wednesday);
            if (Thursday) selectedDays.Add(DayOfWeek.Thursday);
            if (Friday) selectedDays.Add(DayOfWeek.Friday);
            if (Saturday) selectedDays.Add(DayOfWeek.Saturday);
            if (Sunday) selectedDays.Add(DayOfWeek.Sunday);

            // Обновляем поля существующей сущности
            alarm.Time = Time;
            alarm.Status = Status ? AlarmStatus.On : AlarmStatus.Off;
            alarm.DaysOfWeek = selectedDays;
            alarm.Mode = SelectedMode;

            // Вызываем обновление через сервис
            await _alarmService.UpdateAsync(alarm);

            // Возврат на предыдущую страницу
            await Shell.Current.GoToAsync("..");
        }


        private async Task DeleteAlarm()
        {
            if (AlarmId == Guid.Empty) return;

            await _alarmService.DeleteAsync(AlarmId);
            await Shell.Current.GoToAsync("..");
        }
    }
}