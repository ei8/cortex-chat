using ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.Views;

[QueryProperty(nameof(AvatarUrl), Constants.GoToParameterKeys.AvatarUrl)]
public partial class MainPage : ContentPage
{
    public string AvatarUrl
    {
        set
        {
            ((MainViewModel)this.BindingContext).AvatarUrl = value;
        }
    }

    public MainPage(MainViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = mainViewModel;

        this.Loaded += MainPage_Loaded;
    }
    private async void MainPage_Loaded(object sender, EventArgs e)
    {
        var mvm = (MainViewModel)this.BindingContext;
        if (mvm.IsReloading)
            mvm.IsReloading = false;
        mvm.IsReloading = true;
    }
}