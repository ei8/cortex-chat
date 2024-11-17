using ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.Views;

[QueryProperty(nameof(AvatarUrl), Constants.GoToParameterKeys.AvatarUrl)]
public partial class AvatarsPage : ContentPage
{
    public string AvatarUrl
    {
        set
        {
            ((AvatarsViewModel)this.BindingContext).AvatarUrl = value;
        }
    }

    public AvatarsPage(AvatarsViewModel avatarsViewModel)
    {
        InitializeComponent();
        BindingContext = avatarsViewModel;

        this.Loaded += AvatarsPage_Loaded;
    }
    private async void AvatarsPage_Loaded(object sender, EventArgs e)
    {
        var avm = (AvatarsViewModel)this.BindingContext;
        if (avm.IsReloading)
            avm.IsReloading = false;
        avm.IsReloading = true;
    }
}