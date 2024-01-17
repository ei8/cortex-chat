using ei8.Cortex.Chat.Application.Identity;
using ei8.Cortex.Chat.Application.Settings;
using ei8.Cortex.Chat.Domain.Model;
using IdentityModel.OidcClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.Services
{
    public class OidcClientService : IOidcClientService
    {
        private OidcClient oidcClient;
        private readonly ISettingsService settingsService;
        private readonly WebAuthenticatorBrowser webAuthenticatorBrowser;

        public OidcClientService(ISettingsService settingsService, WebAuthenticatorBrowser webAuthenticatorBrowser)
        {
            this.settingsService = settingsService;
            this.webAuthenticatorBrowser = webAuthenticatorBrowser;
        }

        public OidcClient GetOidcClient()
        {
            if (this.oidcClient == null)
            {
                this.oidcClient = new OidcClient(new OidcClientOptions
                {
                    Authority = this.settingsService.Authority,
                    ClientId = this.settingsService.ClientId,
                    Scope = this.settingsService.RequestedScopes,
                    // used in ei8.Cortex.Chat.Port.Adapter.UI.Maui.Platforms.Android.Auth.ChatWebAuthenticatorCallbackActivity
                    RedirectUri = "ei8cortexchat://",
                    PostLogoutRedirectUri = "ei8cortexchat://",
                    ClientSecret = this.settingsService.ClientSecret,
                    HttpClientFactory = options => GetInsecureHttpClient(),
                    Browser = this.webAuthenticatorBrowser
                });
            }

            return this.oidcClient;
        }

        public static HttpClient GetInsecureHttpClient()
        {
            // #if ANDROID
            // TODO: var handler = new CustomAndroidMessageHandler();
            // #else
            var handler = new HttpClientHandler
            {
                // #endif
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
            };
            HttpClient client = new HttpClient(handler);
            return client;
        }

        public void ClearOidcClient()
        {
            this.oidcClient = null;
        }
    }
}
