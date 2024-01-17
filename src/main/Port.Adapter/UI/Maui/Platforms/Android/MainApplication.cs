using Android.App;
using Android.Runtime;

[assembly: UsesPermission(Android.Manifest.Permission.AccessCoarseLocation)]
[assembly: UsesPermission(Android.Manifest.Permission.AccessFineLocation)]
[assembly: UsesFeature("android.hardware.location", Required = false)]
[assembly: UsesFeature("android.hardware.location.gps", Required = false)]
[assembly: UsesFeature("android.hardware.location.network", Required = false)]

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.Platforms.Android;

#if DEBUG                                   // connect to local service on the
[Application(UsesCleartextTraffic = true)]  // emulator's host for debugging,
#else                                       // access via http://10.0.2.2
[Application]                               
#endif
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
        // During development, we can trust all certificates, including ASP.NET developer certificates
        // DO NOT ENABLE THIS IN RELEASE BUILDS
        System.Net.ServicePointManager.ServerCertificateValidationCallback = (_, __, ___, ____) => true;
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}