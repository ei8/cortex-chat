﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ei8.Cortex.Chat.Application.Settings;
using ei8.Cortex.Chat.Port.Adapter.UI.Maui.Services;
using ei8.Cortex.Chat.Port.Adapter.UI.Maui.Views.Auth;
using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels
{
    public partial class SettingsViewModel : ViewModelBase
    {
        private ISettingsService settingsService;
        public SettingsViewModel(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        [ObservableProperty]
        private string clientSecret;

        [RelayCommand]
        public async Task SaveAsync()
        {
            this.settingsService.ClientSecret = this.ClientSecret;

            await Shell.Current.GoToAsync("..", true);
        }
    }
}