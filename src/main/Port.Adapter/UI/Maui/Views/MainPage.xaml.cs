using ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.Views;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = mainViewModel;

        this.Loaded += MainPage_Loaded;
    }
    private async void MainPage_Loaded(object sender, EventArgs e)
    {
        ((MainViewModel)this.BindingContext).IsReloading = true;
    }
}