using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ei8.Cortex.Chat.Application.Identity;
using ei8.Cortex.Chat.Application.Messages;
using ei8.Cortex.Chat.Domain.Model;
using IdentityModel.OidcClient;
using System.Collections.ObjectModel;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels
{
    /// <summary>
    /// Represents a view model for Messages.
    /// </summary>
    /// <remarks>
    /// Constructs a MessageViewModel.
    /// </remarks>
    /// <param name="messageApplicationService">The IMessageApplicationService to modify Messages.</param>
    /// <param name="messageQueryService">The IMessageQueryService to retrieve Messages.</param>
    /// <param name="oidcClientService">The IOidcClientService to be used to retrieve OIDC clients.</param>
    /// <param name="connectivity">The Connectivity interface to monitor network changes.</param>
    public partial class MessagesViewModel(
        IMessageApplicationService messageApplicationService, 
        IMessageQueryService messageQueryService, 
        IOidcClientService oidcClientService, 
        IConnectivity connectivity
    ) : ViewModelBase
    {
        private readonly IMessageApplicationService messageApplicationService = messageApplicationService;
        private readonly IMessageQueryService messageQueryService = messageQueryService;
        private readonly IOidcClientService oidcClientService = oidcClientService;
        private readonly IConnectivity connectivity = connectivity;

        [ObservableProperty]
        private string content;

        [ObservableProperty]
        private bool isReloading;

        /// <summary>
        /// Gets or sets the Messages list.
        /// </summary>
        public ObservableCollection<object> Messages { get; } = new();

        internal Avatar Avatar { get; set; }

        internal string AvatarUrl { get; set; }

        /// <summary>
        /// Reloads the Messages list.
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
                    this.Messages.Clear();

                    var messages = await this.messageQueryService.GetMessagesAsync(
                        this.AvatarUrl + "/", 
                        null, 
                        null,
                        new Guid[] { this.Avatar.Id }
                    );
                    messages.ToList().ForEach(this.Messages.Add);

                    this.IsReloading = false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.ToString(), "Ok");
            }
        }

        /// <summary>
        /// Sends a Message.
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        public async Task SendAsync()
        {
            try
            {
                if (this.connectivity.NetworkAccess is not NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Internet Offline", "Check your internet and try again", "Ok");
                }
                else
                {
                    await this.messageApplicationService.SendMessageAsync(
                       this.AvatarUrl + "/",
                       this.Content,
                       null,
                       null
                   );

                    this.Content = string.Empty;
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
        public async Task LogoutAsync()
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

