using CommunityToolkit.Maui;
using ei8.Cortex.Chat.Port.Adapter.UI.Maui.Services;
using ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels;
using ei8.Cortex.Chat.Port.Adapter.UI.Maui.ViewModels.Auth;
using ei8.Cortex.Chat.Port.Adapter.UI.Maui.Views;
using IdentityModel.OidcClient;
using neurUL.Common.Http;
using ei8.Cortex.Chat.Port.Adapter.IO.Process.Services;
using ei8.Cortex.Chat.Domain.Model;
using ei8.Cortex.Chat.Application.Settings;
using ei8.Cortex.Chat.Application.Identity;
using ei8.Cortex.Chat.Nucleus.Client.In;
using ei8.Cortex.Chat.Nucleus.Client.Out;
using ei8.Cortex.Chat.Application.Messages;
using ei8.Cortex.Chat.Application;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<IRequestProvider, RequestProvider>(sp =>
        {
            var rp = new RequestProvider();
            rp.SetHttpClientHandler(new HttpClientHandler());
            return rp;
        });
        builder.Services.AddSingleton<IMessageClient, HttpMessageClient>();
        builder.Services.AddSingleton<IMessageQueryClient, HttpMessageQueryClient>();
        builder.Services.AddSingleton<IAvatarQueryClient, HttpAvatarQueryClient>();

        builder.Services.AddSingleton(Connectivity.Current);
        builder.Services.AddSingleton<ITokenProviderService, TokenProviderService>();
        builder.Services.AddSingleton<ISettingsService, SettingsService>();
        builder.Services.AddSingleton<IMessageApplicationService, MessageApplicationService>();
        builder.Services.AddSingleton<IMessageQueryService, MessageQueryService>();
        builder.Services.AddSingleton<IAvatarQueryService, AvatarQueryService>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<SettingsViewModel>();
        builder.Services.AddSingleton<MessagesViewModel>();
        builder.Services.AddSingleton<AvatarsViewModel>();
        builder.Services.AddSingleton<Views.Auth.LoginPage>();
        builder.Services.AddSingleton<SettingsPage>();
		builder.Services.AddSingleton<MessagesPage>();
        builder.Services.AddSingleton<AvatarsPage>();

        builder.Services.AddSingleton<HttpClient>(OidcClientService.GetInsecureHttpClient());
        builder.Services.AddTransient<WebAuthenticatorBrowser>();
        builder.Services.AddTransient<IOidcClientService, OidcClientService>();

        return builder.Build();
    }
}