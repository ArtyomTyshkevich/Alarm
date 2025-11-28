using Alarm.Maui.ViewModels;

namespace Alarm.Maui.Views;

public partial class AddAlarmPage : ContentPage
{
	public AddAlarmPage(AddAlarmPageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}