using Android.App;
using Android.Content;
using Android.Content.PM;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.Platforms.Android.Auth;

[Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop, Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
    DataScheme = CALLBACK_SCHEME)]
public class ChatWebAuthenticatorCallbackActivity : WebAuthenticatorCallbackActivity
{
    // used in ei8.Cortex.Chat.Port.Adapter.UI.Maui.Services.OidcClientService
    const string CALLBACK_SCHEME = "ei8cortexchat";
}