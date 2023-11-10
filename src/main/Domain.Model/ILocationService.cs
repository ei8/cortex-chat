using System;

namespace ei8.Cortex.Chat.Domain.Model
{
    public interface ILocationService
    {
        event EventHandler<LocationModel> LocationChanged;

        event EventHandler<string> StatusChanged;

        void Initialize();

        void Stop();
    }
}
