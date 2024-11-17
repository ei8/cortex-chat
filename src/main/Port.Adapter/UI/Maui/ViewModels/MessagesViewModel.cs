using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ei8.Cortex.Chat.Application.Identity;
using ei8.Cortex.Chat.Application.Messages;
using ei8.Cortex.Chat.Domain.Model;
using IdentityModel.OidcClient;
using System.Collections.ObjectModel;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels
{
    public partial class MessagesViewModel : ViewModelBase
    {
        private readonly IMessageApplicationService messageApplicationService;
        private readonly IMessageQueryService messageQueryService;
        protected readonly IOidcClientService oidcClientService;
        protected IConnectivity connectivity;

        [ObservableProperty]
        private string content;

        [ObservableProperty]
        private bool isReloading;

        public MessagesViewModel(IMessageApplicationService messageApplicationService, IMessageQueryService messageQueryService, IOidcClientService oidcClientService, IConnectivity connectivity)
        {
            this.oidcClientService = oidcClientService;
            this.connectivity = connectivity;
            this.messageApplicationService = messageApplicationService;
            this.messageQueryService = messageQueryService;
            this.Messages = new();
        }

        public ObservableCollection<object> Messages { get; }

        internal Avatar Avatar { get; set; }

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
                    this.Messages.Clear();

                    var messages = await this.messageQueryService.GetMessagesAsync(
                        this.AvatarUrl + "/", 
                        null, 
                        null,
                        new Guid[] { this.Avatar.Id }
                    );
                    messages.ToList().ForEach(m => this.Messages.Add(m));

                    this.IsReloading = false;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.ToString(), "Ok");
            }
        }

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

