using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.Views;

[QueryProperty(nameof(Avatar), Constants.GoToParameterKeys.Avatar)]
[QueryProperty(nameof(AvatarUrl), Constants.GoToParameterKeys.AvatarUrl)]
public partial class MessagesPage : ContentPage
{
    public Avatar Avatar
    {
        set
        {
            ((MessagesViewModel)this.BindingContext).Avatar = value;
        }
    }

    public string AvatarUrl
    {
        set
        {
            ((MessagesViewModel)this.BindingContext).AvatarUrl = value;
        }
    }


    public MessagesPage(MessagesViewModel mainViewModel)
    {
        InitializeComponent();
        BindingContext = mainViewModel;

        this.Loaded += MessagesPage_Loaded;
    }
    private async void MessagesPage_Loaded(object sender, EventArgs e)
    {
        var mvm = (MessagesViewModel)this.BindingContext;
        if (mvm.IsReloading)
            mvm.IsReloading = false;
        mvm.IsReloading = true;
    }
}