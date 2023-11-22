using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ei8.Cortex.Chat.Application.Identity;
using ei8.Cortex.Chat.Application.Settings;
using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Chat.Nucleus.Client.In;
using ei8.Cortex.Chat.Nucleus.Client.Out;
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
        private readonly IMessageQueryClient messageQueryClient;
        protected readonly IOidcClientService oidcClientService;
        protected IConnectivity connectivity;
        private readonly ITokenProviderService tokenProviderService;

        [ObservableProperty]
        private string content;

        [ObservableProperty]
        private bool isReloading;

        public MainViewModel(ISettingsService settingsService, IUrlService urlService, IMessageClient messageClient, IMessageQueryClient messageQueryClient,  IOidcClientService oidcClientService, IConnectivity connectivity, ITokenProviderService tokenProviderService)
        {
            this.settingsService = settingsService;
            this.urlService = urlService;
            this.oidcClientService = oidcClientService;
            this.connectivity = connectivity;
            this.Messages = new();
            this.messageClient = messageClient;
            this.messageQueryClient = messageQueryClient;
            this.tokenProviderService = tokenProviderService;
        }

        public ObservableCollection<object> Messages { get; }

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
                    this.IsReloading = true;

                    var messageData = await this.messageQueryClient.GetMessagesAsync(
                        this.urlService.AvatarUrl + "/",
                        this.tokenProviderService.AccessToken
                        );

                    this.Messages.Clear();

                    messageData.Select(md => new Message()
                    {
                        Id = md.Id,
                        Content = md.Content,
                        Region = md.Region,
                        RegionId = md.RegionId,
                        Sender = md.Sender,
                        SenderId = md.SenderId,
                        CreationTimestamp = md.CreationTimestamp,
                        LastModificationTimestamp = md.LastModificationTimestamp
                    }).ToList().ForEach(m => this.Messages.Add(m));

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
            var neuronId = Guid.NewGuid().ToString();
            string regionId = null;
            try
            {
                if (this.connectivity.NetworkAccess is not NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Internet Offline", "Check your internet and try again", "Ok");
                }
                else
                {
                    await this.messageClient.CreateMessage(
                        this.urlService.AvatarUrl + "/",
                        neuronId.ToString(),
                        this.Content,
                        regionId,
                        this.tokenProviderService.AccessToken
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

