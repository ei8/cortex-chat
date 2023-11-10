using ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		this.BindingContext = settingsViewModel;
	}
}