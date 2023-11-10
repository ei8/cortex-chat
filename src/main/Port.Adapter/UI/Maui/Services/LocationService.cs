using ei8.Cortex.Chat.Domain.Model;
using System;

namespace ei8.Cortex.Chat.Port.Adapter.UI.Maui.Services;

public partial class LocationService : ILocationService
{
    public event EventHandler<LocationModel> LocationChanged;
    public event EventHandler<string> StatusChanged;
    
    public void Initialize()
    {
#if ANDROID
        AndroidInitialize();
#elif IOS
        IosInitialize();
#endif
    }

    public void Stop()
    {
#if ANDROID
        AndroidStop();
#elif IOS
        IosStop();
#endif
    }

    protected virtual void OnLocationChanged(LocationModel e)
    {
        LocationChanged?.Invoke(this, e);
    }

    protected virtual void OnStatusChanged(string e)
    {
        StatusChanged?.Invoke(this, e);
    }
}