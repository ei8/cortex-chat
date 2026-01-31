using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ei8.Cortex.Chat.Application;
using ei8.Cortex.Chat.Application.Identity;
using ei8.Cortex.Chat.Port.Adapter.UI.Maui.Views;
using IdentityModel.OidcClient;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels
{
    public partial class AvatarsViewModel : ViewModelBase
    {
        private readonly IAvatarQueryService avatarQueryService;
        protected readonly IOidcClientService oidcClientService;
        protected readonly IConnectivity connectivity;

        [ObservableProperty]
        private string content;

        [ObservableProperty]
        private bool isReloading;

        public AvatarsViewModel(IAvatarQueryService avatarQueryService, IOidcClientService oidcClientService, IConnectivity connectivity)
        {
            this.oidcClientService = oidcClientService;
            this.connectivity = connectivity;
            this.avatarQueryService = avatarQueryService;
            this.Avatars = new();

            this.SelectAvatar = new Command(async (a) => 
                await Shell.Current.GoToAsync(
                    $"{nameof(MessagesPage)}", 
                    true, 
                    new Dictionary<string, object>() {
                        { Constants.GoToParameterKeys.Avatar, a },
                        { Constants.GoToParameterKeys.AvatarUrl, this.AvatarUrl }
                    }
                ));
        }

        public ICommand SelectAvatar { get; private set; }

        public ObservableCollection<object> Avatars { get; }

        internal string AvatarUrl { get; set; }

        [RelayCommand]
        public async Task ReloadAsync()
        {
            try
            {
                if (this.connectivity.NetworkAccess is not NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Internet Offline", "Check your internet and try again", "Ok");
                }
                else
                {
                    this.Avatars.Clear();

                    var avs = await this.avatarQueryService.GetAvatarsAsync(this.AvatarUrl + "/");
                    avs.ToList().ForEach(av => this.Avatars.Add(av.First()));

                    this.IsReloading = false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.ToString(), "Ok");
            }
        }

        [RelayCommand]
        async Task LogoutAsync()
        {
            try
            {
                var loginResult = await this.oidcClientService.GetOidcClient().LogoutAsync(new LogoutRequest());
                await Shell.Current.DisplayAlert("Result", "Success", "OK");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.ToString(), "Ok");
            }
        }
    }
}

