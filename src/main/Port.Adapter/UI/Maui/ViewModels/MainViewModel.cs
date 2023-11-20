using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ei8.Cortex.Chat.Application.Identity;
using ei8.Cortex.Chat.Application.Settings;
using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Chat.Nucleus.Client.In;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        private readonly ISettingsService settingsService;
        private readonly IUrlService urlService;
        private readonly IMessageClient messageClient;
        protected readonly IOidcClientService oidcClientService;
        protected readonly HttpClient httpClient;
        protected IConnectivity connectivity;
        private readonly ITokenProviderService tokenProviderService;

        [ObservableProperty]
        private bool locationUpdatesEnabled;

        public MainViewModel(ISettingsService settingsService, IUrlService urlService, IMessageClient messageClient, IOidcClientService oidcClientService, HttpClient httpclient, IConnectivity connectivity, ITokenProviderService tokenProviderService)
        {
            this.settingsService = settingsService;
            this.urlService = urlService;
            this.oidcClientService = oidcClientService;
            this.httpClient = httpclient;
            this.connectivity = connectivity;
            Updates = new();
            this.messageClient = messageClient;
            this.tokenProviderService = tokenProviderService;
        }

        public ObservableCollection<object> Updates { get; }
        
        [RelayCommand]
        public async Task UploadLastLocationAsync()
        {
            var neuronId = Guid.NewGuid().ToString();
            string regionId = null;
            try
            {
                var task = Task.Run(async () => await this.messageClient.CreateMessage(
                    this.urlService.AvatarUrl + "/",
                    neuronId.ToString(),
                    "Hello world!!!",
                    regionId,
                    this.tokenProviderService.AccessToken
                    ));
                task.GetAwaiter().GetResult();
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

