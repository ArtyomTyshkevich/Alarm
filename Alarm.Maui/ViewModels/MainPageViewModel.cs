using Alarm.Application.Interfaces.Services;
using Alarm.Core.Models;
using Alarm.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Alarm.Maui.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IAlarmUnitService _alarmService;

        public ObservableCollection<AlarmUnit> Alarms { get; } = new();

        public IRelayCommand AddAlarmCommand { get; }
        public IRelayCommand<AlarmUnit> OpenAlarmCommand { get; }
        public IRelayCommand LoadAlarmsCommand { get; }

        public MainPageViewModel(IAlarmUnitService alarmService)
        {
            _alarmService = alarmService;

            AddAlarmCommand = new AsyncRelayCommand(OnAddAlarm);
            OpenAlarmCommand = new AsyncRelayCommand<AlarmUnit>(OnOpenAlarm);
            LoadAlarmsCommand = new AsyncRelayCommand(LoadAlarmsAsync);

            LoadAlarmsCommand.Execute(null);
        }

        public async Task LoadAlarmsAsync()
        {
            var alarmsFromDb = await _alarmService.GetAllAsync();
            Alarms.Clear();
            foreach (var alarm in alarmsFromDb)
            {
                Alarms.Add(alarm);
            }
        }

        private async Task OnAddAlarm()
        {
            await Shell.Current.GoToAsync(nameof(AddAlarmPage));
        }

        private async Task OnOpenAlarm(AlarmUnit alarm)
        {
            if (alarm == null) return;

            await Shell.Current.GoToAsync($"{nameof(UpdateAlarmPage)}?AlarmIdString={alarm.Id}");
        }
    }
}
