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
    /// <summary>
    /// Represents a view model for Avatars.
    /// </summary>
    public partial class AvatarsViewModel : ViewModelBase
    {
        private readonly IAvatarQueryService avatarQueryService;
        private readonly IOidcClientService oidcClientService;
        private readonly IConnectivity connectivity;

        [ObservableProperty]
        private string content;

        [ObservableProperty]
        private bool isReloading;

        /// <summary>
        /// Constructs an AvatarsViewModel.
        /// </summary>
        /// <param name="avatarQueryService">The IAvatarQueryService to retrieve Avatars.</param>
        /// <param name="oidcClientService">The IOidcClientService to be used to retrieve OIDC clients.</param>
        /// <param name="connectivity">The Connectivity interface to monitor network changes.</param>
        public AvatarsViewModel(
            IAvatarQueryService avatarQueryService, 
            IOidcClientService oidcClientService, 
            IConnectivity connectivity
        )
        {
            this.oidcClientService = oidcClientService;
            this.connectivity = connectivity;
            this.avatarQueryService = avatarQueryService;
            this.Avatars = [];

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

        /// <summary>
        /// Gets or sets the SelectAvatar command.
        /// </summary>
        public ICommand SelectAvatar { get; private set; }

        /// <summary>
        /// Gets or sets the Avatars list.
        /// </summary>
        public ObservableCollection<object> Avatars { get; }

        internal string AvatarUrl { get; set; }

        /// <summary>
        /// Reloads the Avatars list.
        /// </summary>
        /// <returns></returns>
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
        
        /// <summary>
        /// Logs User out.
        /// </summary>
        /// <returns></returns>
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