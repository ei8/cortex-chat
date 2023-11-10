using ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels.Auth;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.Views.Auth;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginviewmodel)
	{
		InitializeComponent();
		BindingContext = loginviewmodel;
	}
}
