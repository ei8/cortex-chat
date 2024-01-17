namespace ei8.Cortex.Chat.Application.Settings
{
    public interface ISettingsService
    {
        string Authority { get; set; }

        string ClientId { get; set; }

        string ClientSecret { get; set; }

        string RequestedScopes { get; set; }
    }
}
