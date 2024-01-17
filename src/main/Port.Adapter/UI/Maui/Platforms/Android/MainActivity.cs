using Android.App;
using Android.Content.PM;
using Android.OS;
using Javax.Net.Ssl;
using Org.Apache.Http.Conn.Ssl;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.Platforms.Android;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
    #if DEBUG
        HttpsURLConnection.DefaultHostnameVerifier = new AllowAllHostnameVerifier();
    #endif
        base.OnCreate(savedInstanceState);
    }
}