using ei8.Cortex.Chat.Application.Settings;

namespace ei8.Cortex.Chat.Port.Adapter.IO.Process.Services
{
    public class SettingsService : ISettingsService
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RequestedScopes { get; set; }
    }
}
