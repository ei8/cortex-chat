using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels;

public partial class ViewModelBase : ObservableObject
{
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    [ObservableProperty]
    bool isBusy;
    
    public bool IsNotBusy => !IsBusy;
}