using ei8.Cortex.Chat.Port.Adapter.UI.Maui.Views;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(AvatarsPage), typeof(AvatarsPage));
        Routing.RegisterRoute(nameof(MessagesPage), typeof(MessagesPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
    }
}