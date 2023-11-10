using ei8.Cortex.Chat.Port.Adapter.UI.Maui.Views;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}