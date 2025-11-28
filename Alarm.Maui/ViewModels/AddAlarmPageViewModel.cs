using Alarm.Application.Interfaces.Services;
using Alarm.Core.Enums;
using Alarm.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Alarm.Maui.ViewModels
{
    public partial class AddAlarmPageViewModel : ObservableObject
    {
        private readonly IAlarmUnitService _alarmService;
        private readonly IAlarmModeService _alarmModeService;

        [ObservableProperty]
        private TimeSpan time = TimeSpan.FromHours(8);

        public ObservableCollection<AlarmMode> Modes { get; } = new();

        [ObservableProperty]
        private AlarmMode selectedMode;

        [ObservableProperty] private bool monday;
        [ObservableProperty] private bool tuesday;
        [ObservableProperty] private bool wednesday;
        [ObservableProperty] private bool thursday;
        [ObservableProperty] private bool friday;
        [ObservableProperty] private bool saturday;
        [ObservableProperty] private bool sunday;

        public IRelayCommand SaveCommand { get; }

        public AddAlarmPageViewModel(IAlarmUnitService alarmService, IAlarmModeService alarmModeService)
        {
            _alarmService = alarmService;
            _alarmModeService = alarmModeService;

            SaveCommand = new AsyncRelayCommand(SaveAlarm);

            LoadModesAsync().ConfigureAwait(false);
        }

        private async Task LoadModesAsync()
        {
            var modes = await _alarmModeService.GetAllAsync();
            Modes.Clear();
            foreach (var mode in modes)
            {
                Modes.Add(mode);
            }

            SelectedMode = Modes.Count > 0 ? Modes[0] : null;
        }

        private async Task SaveAlarm()
        {
            var selectedDays = new List<DayOfWeek>();
            if (Monday) selectedDays.Add(DayOfWeek.Monday);
            if (Tuesday) selectedDays.Add(DayOfWeek.Tuesday);
            if (Wednesday) selectedDays.Add(DayOfWeek.Wednesday);
            if (Thursday) selectedDays.Add(DayOfWeek.Thursday);
            if (Friday) selectedDays.Add(DayOfWeek.Friday);
            if (Saturday) selectedDays.Add(DayOfWeek.Saturday);
            if (Sunday) selectedDays.Add(DayOfWeek.Sunday);

            var alarm = new AlarmUnit
            {
                Id = Guid.NewGuid(),
                Time = Time,
                Mode = SelectedMode,
                Status = AlarmStatus.On,
                DaysOfWeek = selectedDays
            };

            await _alarmService.AddAsync(alarm);

            await Shell.Current.GoToAsync("..");
        }
    }
}
