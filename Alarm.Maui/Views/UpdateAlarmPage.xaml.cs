using Alarm.Maui.ViewModels;

namespace Alarm.Maui.Views;

public partial class UpdateAlarmPage : ContentPage
{
	public UpdateAlarmPage(UpdateAlarmPageViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
    }
}