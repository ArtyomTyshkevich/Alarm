using Alarm.Maui.ViewModels;

namespace Alarm.Maui.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is MainPageViewModel vm)
            vm.LoadAlarmsCommand.Execute(null);
    }

}
