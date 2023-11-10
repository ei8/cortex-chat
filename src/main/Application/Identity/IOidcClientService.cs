using IdentityModel.OidcClient;

namespace ei8.Cortex.Chat.Application.Identity
{
    public interface IOidcClientService
    {
        OidcClient GetOidcClient();

        void ClearOidcClient();
    }
}
